﻿using System.Web.Http;

namespace MozuDataConnector.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{tenantId}", new { tenantId = RouteParameter.Optional, action = "Default" }, new { id = @"\d*" });

        }
    }
}
