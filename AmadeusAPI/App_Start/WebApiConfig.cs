using AmadeusAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AmadeusAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {   //CORS
            //config.EnableCors();

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new BasicAuthenticationAttribute());
        }
    }
}
