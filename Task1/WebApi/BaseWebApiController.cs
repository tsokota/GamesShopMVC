using BusinessLogicLayer.Services.IServices;
using Model;
using Model.Entities;
using Ninject;
using NLog.Interface;
using System.Globalization;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using Yevhenii_KoliesnikTask1.Authorization;
using Yevhenii_KoliesnikTask1.Authorization.AuthorInterface;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.WebApi
{
    [ExceptionControllerAttribute]
    public class BaseWebApiController : ApiController
    {
        public string CurrentLangCode { get; protected set; }

        [Inject]
        public IAuthentication Auth { get; set; }

        [Inject]
        public ILanguageService LanguageService { get; set; }

        public ILogger Logger { get; set; }

        public BaseWebApiController(ILogger logger)
        {
            Logger = logger;
        }

        public User CurrentUser
        {
            get
            {
                return ((UserIndentity)Auth.CurrentUser.Identity).User;
            }
        }

        protected override void Initialize(HttpControllerContext requestContext)
        {
            if (requestContext.RouteData.Values[SettingsConst.LangApi] != null && requestContext.RouteData.Values[SettingsConst.LangApi] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values[SettingsConst.LangApi] as string;
                var ci = new CultureInfo(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            base.Initialize(requestContext);
        }
    }
}

