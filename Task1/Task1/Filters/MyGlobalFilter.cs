using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yevhenii_KoliesnikTask1.Filters
{
    public class MyGlobalFilter : IActionFilter
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // Use global filter to log IP of requests in txt file.
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            logger.Info(System.Web.HttpContext.Current.Request.UserHostAddress + "IP some request");
        }

        // test not work with Opera
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Browser.Browser == "Opera")
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}