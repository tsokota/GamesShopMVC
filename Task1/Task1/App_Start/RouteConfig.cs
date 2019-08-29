using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yevhenii_KoliesnikTask1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // for downloading (file must be founded by this path)
            routes.IgnoreRoute("game/{gamekey}/download", new { gamekey = @"\d+" });

            routes.MapRoute(
                name: "Games",
                url: "games/{action}",
                defaults: new { controller = "Game", action = "AllGames" }
            );

            routes.MapRoute(
                name: "GameDetails",
                url: "game/{gamekey}/{action}",
                defaults: new { controller = "Game", action = "GameDetails", gamekey = UrlParameter.Optional},
                constraints: new { gamekey = @"\d+" }
            );


            // default rulls (just don't touch it)
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}