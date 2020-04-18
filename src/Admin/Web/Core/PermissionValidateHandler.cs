﻿using System.Collections.Generic;
using System.Linq;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Enums;
using NetModular.Module.Admin.Application.AccountService;

namespace NetModular.Module.Admin.Web.Core
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class PermissionValidateHandler : IPermissionValidateHandler
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAccountService _accountService;
        private readonly IConfigProvider _configProvider;

        public PermissionValidateHandler(IAccountService accountService, ILoginInfo loginInfo, IConfigProvider configProvider)
        {
            _accountService = accountService;
            _loginInfo = loginInfo;
            _configProvider = configProvider;
        }

        public bool Validate(IDictionary<string, string> routeValues, HttpMethod httpMethod)
        {
            var permissions = _accountService.QueryPermissionList(_loginInfo.AccountId, _loginInfo.Platform).Result;

            var area = routeValues["area"];
            var controller = routeValues["controller"];
            var action = routeValues["action"];
            return permissions.Any(m => m.EqualsIgnoreCase($"{area}_{controller}_{action}_{httpMethod}"));
        }
    }
}
