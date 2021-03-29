using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAWProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GameResult",
                url: "GameResult/{action}/{gameId}/{playerId}",
                defaults: new { controller = "GameResult", action = "Index", gameId = UrlParameter.Optional, playerId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "GameResult", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
