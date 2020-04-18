﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Entities.Extend;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Lib.Data.Core.Entities
{
    /// <summary>
    /// 实体描述
    /// </summary>
    public class EntityDescriptor : IEntityDescriptor
    {
        #region ==属性==

        /// <summary>
        /// 数据库配置
        /// </summary>
        public DbOptions DbOptions { get; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        public DbModuleOptions ModuleOptions { get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// 不创建表
        /// </summary>
        public bool Ignore { get; private set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; }

        /// <summary>
        /// 列
        /// </summary>
        public IColumnDescriptorCollection Columns { get; private set; }

        /// <summary>
        /// 主键
        /// </summary>
        public IPrimaryKeyDescriptor PrimaryKey { get; private set; }

        public EntitySql Sql { get; }

        public ISqlAdapter SqlAdapter { get; }

        /// <summary>
        /// 是否包含基类
        /// </summary>
        public bool IsEntityBase { get; }

        /// <summary>
        /// 是否包含软删除
        /// </summary>
        public bool IsSoftDelete { get; }

        /// <summary>
        /// 是否包含多租户
        /// </summary>
        public bool IsTenant { get; set; } = true;

        /// <summary>
        /// 租户编号字段名称
        /// </summary>
        public string TenantIdColumnName { get; set; } = "TenantId";

        #endregion

        #region ==构造器==

        public EntityDescriptor(DbOptions dbOptions, DbModuleOptions dbModuleOptions, Type entityType, ISqlAdapter sqlAdapter)
        {
            DbOptions = dbOptions;

            ModuleOptions = dbModuleOptions;

            ModuleName = dbModuleOptions.Name;

            SqlAdapter = sqlAdapter;

            EntityType = entityType;

            Database = sqlAdapter.Database;

            PrimaryKey = new PrimaryKeyDescriptor();

            //实体基类
            IsEntityBase = EntityType.GetInterfaces().Any(m => m == typeof(IBase));

            //软删除
            IsSoftDelete = EntityType.GetInterfaces().Any(m => m == typeof(ISoftDelete));

            SetTableName();

            SetIgnore();

            SetColumns();

            var sqlBuilder = new EntitySqlBuilder(this);
            Sql = sqlBuilder.Build();
        }

        #endregion

        #region ==私有方法==

        /// <summary>
        /// 设置表名
        /// </summary>
        private void SetTableName()
        {
            var tableArr = EntityType.GetCustomAttribute<TableAttribute>(false);

            if (tableArr != null)
            {
                TableName = tableArr.Name;

                //多租户
                IsTenant = tableArr.IsTenant;
                TenantIdColumnName = tableArr.TenantIdColumnName;
            }
            else
            {
                TableName = EntityType.Name;
                //去掉Entity后缀
                if (TableName.EndsWith("Entity"))
                {
                    TableName = TableName.Substring(0, TableName.Length - 6);
                }
            }

            //判断有没有设置前缀
            if (SqlAdapter.Options.Prefix.NotNull())
            {
                var prefixIgnoreArr = EntityType.GetCustomAttribute<TableAttribute>(false);
                if (prefixIgnoreArr != null)
                {
                    TableName = SqlAdapter.Options.Prefix + TableName;
                }
            }

            if (SqlAdapter.ToLower)
            {
                TableName = TableName.ToLower();
            }
        }

        /// <summary>
        /// 设置是否不创建表
        /// </summary>
        private void SetIgnore()
        {
            var ignoreArr = EntityType.GetCustomAttribute<IgnoreAttribute>(false);
            Ignore = ignoreArr != null;
        }

        /// <summary>
        /// 设置属性列表
        /// </summary>
        private void SetColumns()
        {
            Columns = new ColumnDescriptorCollection();

            //加载属性列表
            var properties = new List<PropertyInfo>();
            foreach (var p in EntityType.GetProperties())
            {
                var type = p.PropertyType;
                if ((!type.IsGenericType || type.IsNullable()) && (type.IsGuid() || type.IsNullable() || Type.GetTypeCode(type) != TypeCode.Object)
                    && Attribute.GetCustomAttributes(p).All(attr => attr.GetType() != typeof(IgnoreAttribute)))
                {
                    properties.Add(p);
                }
            }

            foreach (var p in properties)
            {
                var column = new ColumnDescriptor(p);
                if (column.IsPrimaryKey)
                {
                    PrimaryKey = new PrimaryKeyDescriptor(p);
                    Columns.Insert(0, column);
                }
                else
                {
                    Columns.Add(column);
                }
            }
        }

        #endregion
    }
}
