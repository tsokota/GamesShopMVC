using BusinessLogicLayer.Services.IServices;
using Model;
using Model.Entities;
using Ninject;
using NLog.Interface;
using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Yevhenii_KoliesnikTask1.Authorization.AuthorInterface;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1.Controllers
{

    [ExceptionControllerAttribute]
    public class BaseController : Controller
    {
        [Inject]
        public IAuthentication Auth { get; set; }
        public ILogger Logger;
        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }
        public BaseController(ILogger logger)
        {
           Logger = logger;
        }
        public string CurrentLangCode { get; protected set; }
        public Language CurrentLang { get; protected set; }
        [Inject]
        public ILanguageService LanguageService { get; set; }
        protected override void Initialize(RequestContext requestContext)
        {

            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string;
                CurrentLang = LanguageService.Get(CurrentLangCode);

                var ci = new CultureInfo(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            base.Initialize(requestContext);

        }

        /// <summary>
        /// this method parser request string and change language for current user,
        /// not redirect anouther page
        /// </summary>
        /// <param name="langCode"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult ChangeCulture(string langCode, string returnUrl)
        {
            try
            {
                string oldCultName = Thread.CurrentThread.CurrentCulture.Name;
                var culture = new CultureInfo(langCode);
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
                string cult = oldCultName.Substring(0, 2);
                string oldLangUrlPart = "/" + cult + "/";
                string newLangUrlPart = "/" + langCode + "/";
                if (returnUrl.Contains(oldLangUrlPart))
                {
                    returnUrl = returnUrl.Replace(oldLangUrlPart, newLangUrlPart);
                }
                if (returnUrl == "/" || returnUrl == "/" + oldCultName)
                {
                    returnUrl = newLangUrlPart;
                }
                return Redirect(returnUrl);
            }
            catch (Exception)
            {

                return new HttpNotFoundResult();
            }
        }
    }
}
