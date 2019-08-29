using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yevhenii_KoliesnikTask1.Filters
{
    public class MyAttributeFilter : ActionFilterAttribute
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Stopwatch timer = new Stopwatch();
        // Use filters for logging performance of services working.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) 
        {
            timer.Stop();
            logger.Info("MyAttributeFilter: " + timer.Elapsed);
        }
    }
}