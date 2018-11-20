using Jwell.UnifiedConfig.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ActionHandleAttribute());
            config.Filters.Add(new ExceptionHandleAttribute());
        }
    }
}
