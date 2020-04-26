﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Domain.AuditInfo.Models;
using NetModular.Module.Admin.Domain.Module;
using NetModular.Module.Admin.Infrastructure.Repositories.SqlServer.Sql;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AuditInfoRepository : RepositoryAbstract<AuditInfoEntity>, IAuditInfoRepository
    {
        public AuditInfoRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<AuditInfoEntity>> Query(AuditInfoQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.Platform, m => m.Platform == model.Platform.Value);
            query.WhereNotNull(model.ModuleCode, m => m.Area == model.ModuleCode);
            query.WhereNotNull(model.Controller, m => m.Controller.Contains(model.Controller) || m.ControllerDesc.Contains(model.Controller));
            query.WhereNotNull(model.Action, m => m.ActionDesc.Contains(model.Action) || m.Action.Contains(model.Action));
            query.WhereNotNull(model.StartDate, m => m.ExecutionTime >= model.StartDate.Value.Date);
            query.WhereNotNull(model.EndDate, m => m.ExecutionTime < model.EndDate.Value.AddDays(1).Date);

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(x => x.Id);
            }

            //导出全部
            if (model.IsExport && model.Export.Mode == ExportMode.All)
            {
                model.ExportCount = await query.CountAsync();
                if (model.IsOutOfExportCountLimit)
                {
                    return new List<AuditInfoEntity>();
                }
                return await query.ToListAsync();
            }

            var list = await query.PaginationAsync(paging);
            model.ExportCount = list.Count;
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<AuditInfoEntity> Details(int id)
        {
            return Db.Find(m => m.Id == id)
                .LeftJoin<AccountEntity>((x, y) => x.AccountId == y.Id)
                .LeftJoin<ModuleEntity>((x, y, z) => x.Area == z.Code)
                .Select((x, y, z) => new
                {
                    x,
                    Creator = y.Name,
                    ModuleName = z.Name
                }).FirstAsync<AuditInfoEntity>();
        }

        public virtual Task<IEnumerable<ChartDataResultModel>> QueryLatestWeekPv()
        {
            var sql = string.Format(AuditInfoSql.QueryLatestWeekPv, Db.EntityDescriptor.TableName);
            return Db.QueryAsync<ChartDataResultModel>(sql);
        }

        public Task<IList<OptionResultModel>> QueryCountByModule()
        {
            return Db.Find().GroupBy(m => new { m.Area }).OrderByDescending(m => m.Count())
                .Select(m => new { Label = m.Key.Area, Value = m.Count() }).ToListAsync<OptionResultModel>();
        }
    }
}
