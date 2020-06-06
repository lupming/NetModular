﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Module.AspNetCore;
using Microsoft.Extensions.Configuration;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Config.Abstractions.Impl;
using NetModular.Lib.Module.Abstractions;
using NetModular.Module.Admin.Application.ModuleService;

namespace NetModular.Module.Admin.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services, IModuleCollection modules, IHostEnvironment env, IConfiguration cfg)
        {
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            UseUploadFile(app);

            //同步模块信息
            app.ApplicationServices.GetService<IModuleService>().Sync();
        }

        public void ConfigureMvc(MvcOptions mvcOptions)
        {
        }

        /// <summary>
        /// 启用上传文件访问权限
        /// </summary>
        /// <param name="app"></param>
        private void UseUploadFile(IApplicationBuilder app)
        {
            var configProvider = app.ApplicationServices.GetService<IConfigProvider>();
            var config = configProvider.Get<PathConfig>();
            var logoPath = Path.Combine(config.UploadPath, "Admin/OSS/Open");
            if (!Directory.Exists(logoPath))
            {
                Directory.CreateDirectory(logoPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(logoPath),
                RequestPath = "/oss/o"
            });
        }
    }
}
