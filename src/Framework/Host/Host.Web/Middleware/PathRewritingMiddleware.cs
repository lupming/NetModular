using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetModular.Lib.Host.Web.Middleware
{
    public class PathRewritingMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly string _domain;

        private readonly string _path;


        public PathRewritingMiddleware(RequestDelegate next, string domain, string path)
        {
            _next = next;

            _domain = domain;

            _path = path;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Equals("/") || context.Request.Path.Equals(""))
            {
                string host = context.Request.Host.Value;

                if (host.Equals(_domain, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Request.Path = _path;
                }
            }
            return _next(context);
        }
    }
}
