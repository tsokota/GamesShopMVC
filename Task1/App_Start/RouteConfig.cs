using System.Data;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yevhenii_KoliesnikTask1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Orders
            routes.MapRoute(
               name: "OrderActions",
               url: "{lang}/orders/{action}",
               defaults: new { controller = "Order", action = "Index", lang = "en" },
               constraints: new { lang = @"ru|en", action = @"Index|MakeOrder|history" }
            );
            routes.MapRoute(
                name: "ToBusket",
                url: "{lang}/busket",
                defaults: new { controller = "Order", action = "GoToBusket", idOrder = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );
            routes.MapRoute(
                name: "PaymentsRoute",
                url: "{lang}/MakeOrder/{action}",
                defaults: new { controller = "Order", action = "Visa", lang = "en" },
                constraints: new { lang = @"ru|en", action = @"Visa|Bank|IBOX" }
            );
       
            #endregion

            routes.MapRoute(
                name: "Comments",
                url: "{lang}/game/{gamekey}/comments/{action}",
                defaults: new { controller = "Comment", action = "GameComments", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new {  lang = @"ru|en" }
            );

            #region Games
            routes.MapRoute(
                name: "GameActions",
                url: "{lang}/game/{gamekey}/{action}",
                defaults: new { controller = "Game", action = "GameDetails", gamekey = UrlParameter.Optional, lang = "en" },
                constraints: new { gamekey = @"\d+", lang = @"ru|en" }
            );

            routes.MapRoute(
                name: "Games",
                url: "{lang}/games/{action}",
                defaults: new { controller = "Game", action = "AllGames", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );


            #endregion

            #region Publishers
            routes.MapRoute(
              name: "PublisherActionsRout",
              url: "{lang}/publisher/{action}",
              defaults: new { controller = "Publisher", lang = "en" },
              constraints: new { action = @"new|update|delete", lang = @"ru|en" }
            );

            routes.MapRoute(
              name: "PublisherDetails",
              url: "{lang}/publisher/{CompanyName}",
              defaults: new
              {
                  controller = "Publisher",
                  action = "PublisherDetails",
                  CompanyName = UrlParameter.Optional,
                  lang = "en"
              },
              constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
              name: "AllPublishers",
              url: "{lang}/publishers/",
              defaults: new { controller = "Publisher", action = "AllPublishers", lang = "en" },
              constraints: new { lang = @"ru|en" }
            );
            #endregion

            // default rulls (just don't touch it)
            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "AllGames", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

        }
    }
}