using System.Web.Http;

namespace Yevhenii_KoliesnikTask1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "AuthApi",
                routeTemplate: "api/account/{action}",
                defaults: new { action = "login", controller = "account" }
            );

            config.Routes.MapHttpRoute(
                name: "OrdersApi",
                routeTemplate: "api/orders/{orderId}",
                defaults: new { lang = "en", orderId = RouteParameter.Optional, controller = "orders" }
            );




             config.Routes.MapHttpRoute(
              name: "commentsApi2",
              routeTemplate: "api/{lang}/games/{gamekey}/{controller}/{action}/{id}",
              defaults: new { controller = "Comments", id = RouteParameter.Optional, lang = "en" },
              constraints: new { lang = @"ru|en" }
             );

            config.Routes.MapHttpRoute(
                name: "CommentsApi",
                routeTemplate: "api/games/{gamekey}/comments/{id}",
                defaults: new { gamekey = RouteParameter.Optional, lang = "en", id = RouteParameter.Optional, controller = "Comments" }
            );




            config.Routes.MapHttpRoute(
                name: "GenresApi",
                routeTemplate: "api/games/{gamekey}/genres",
                defaults: new { gamekey = RouteParameter.Optional, lang = "en", controller = "games", action = "GetGenres" }
            );

            config.Routes.MapHttpRoute(
                name: "GamesApi",
                routeTemplate: "api/{controller}/{name}/games",
                defaults: new { name = RouteParameter.Optional, lang = "en", controller = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{gamekey}",
                defaults: new { gamekey = RouteParameter.Optional, lang = "en" }
            );

        }
    }
}
