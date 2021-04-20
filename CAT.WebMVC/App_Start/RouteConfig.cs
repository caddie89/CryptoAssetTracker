using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CAT.WebMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "MomentShowcaseDetails",
                url: "{controller}/{action}/{momentId}/{showcaseId}",
                defaults: new { controller = "Home", action = "Details", moment = UrlParameter.Optional, showcase = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "MomentShowcaseDelete",
                url: "{controller}/{action}/{momentId}/{showcaseId}",
                defaults: new { controller = "Home", action = "Delete", moment = UrlParameter.Optional, showcase = UrlParameter.Optional }
                );
        }
    }
}
