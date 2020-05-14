﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Lib.Auth.Web.Attributes
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionValidateAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //排除匿名访问
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
                return;

            var configProvider = context.HttpContext.RequestServices.GetService<IConfigProvider>();
            var config = configProvider.Get<AuthConfig>();
            //是否开启权限认证
            if (!config.Validate)
                return;

            //是否启用单账户登录
            if (config.SingleAccount)
            {
                var singleAccountLoginHandler = context.HttpContext.RequestServices.GetService<ISingleAccountLoginHandler>();
                if (singleAccountLoginHandler != null && await singleAccountLoginHandler.Validate())
                {
                    context.Result = new ContentResult();
                    context.HttpContext.Response.StatusCode = 622;//自定义状态码来判断是否是在其他地方登录
                    return;
                }
            }

            //未登录
            var loginInfo = context.HttpContext.RequestServices.GetService<ILoginInfo>();
            if (loginInfo == null || loginInfo.AccountId.IsEmpty())
            {
                context.Result = new ChallengeResult();
                return;
            }

            //排除通用接口
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(CommonAttribute)))
                return;

            var httpMethod = (HttpMethod)Enum.Parse(typeof(HttpMethod), context.HttpContext.Request.Method);
            var handler = context.HttpContext.RequestServices.GetService<IPermissionValidateHandler>();

            if (!await handler.Validate(context.ActionDescriptor.RouteValues, httpMethod))
            {
                //无权访问
                context.Result = new ForbidResult();
            }
        }
    }
}
