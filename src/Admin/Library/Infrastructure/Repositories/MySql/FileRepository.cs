﻿using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class FileRepository : SqlServer.FileRepository
    {
        public FileRepository(IDbContext context) : base(context)
        {
        }
    }
}
