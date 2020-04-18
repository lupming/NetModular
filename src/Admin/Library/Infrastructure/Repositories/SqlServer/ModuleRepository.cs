﻿using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.Module;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ModuleRepository : RepositoryAbstract<ModuleEntity>, IModuleRepository
    {
        public ModuleRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> Exists(ModuleEntity entity, IUnitOfWork uow)
        {
            var query = Db.Find(m => m.Number == entity.Number && m.Code == entity.Code);
            query.WhereNotEmpty(entity.Id, m => m.Id != entity.Id);
            query.UseUow(uow);
            return query.ExistsAsync();
        }

        public Task<bool> UpdateByCode(ModuleEntity entity)
        {
            return Db.Find().Where(m => m.Code == entity.Code).UpdateAsync(m => new ModuleEntity
            {
                Number = entity.Number,
                Name = entity.Name,
                Icon = entity.Icon,
                Version = entity.Version,
                Remarks = entity.Remarks
            });
        }

        public Task<bool> UpdateApiRequestCount(string code, long count, IUnitOfWork uow = null)
        {
            return Db.Find(m => m.Code == code).UseUow(uow)
                .UpdateAsync(m => new ModuleEntity { ApiRequestCount = count });
        }
    }
}
