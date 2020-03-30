﻿using System.Threading.Tasks;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Domain.Config.Models;

namespace NetModular.Module.Admin.Application.ConfigService
{
    /// <summary>
    /// 配置项服务
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ConfigQueryModel model);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(ConfigAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(int id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(ConfigUpdateModel model);

        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type">配置类型</param>
        /// <param name="moduleCode">模块编码，仅获取模块配置时有用</param>
        /// <returns></returns>
        Task<IResultModel> GetValueByKey(string key, ConfigType type = ConfigType.System, string moduleCode = null);
    }
}
