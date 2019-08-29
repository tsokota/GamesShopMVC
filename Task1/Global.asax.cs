using DAL;
using System.Data.Entity;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Yevhenii_KoliesnikTask1.Mappers;

namespace Yevhenii_KoliesnikTask1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                 new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));

            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer(new GameStoreContextDbInitializer());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Creater map rule for WebApi
            MainMapper.MainMappers();
        }
    }
}