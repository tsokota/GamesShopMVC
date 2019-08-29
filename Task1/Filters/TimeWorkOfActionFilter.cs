using System.Diagnostics;
using System.Web.Mvc;

namespace Yevhenii_KoliesnikTask1.Filters
{
    public class TimeWorkOfActionFilter : ActionFilterAttribute
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public Stopwatch Timer = new Stopwatch();
        // Use filters for logging performance of services working.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Timer.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) 
        {
            Timer.Stop();
            _logger.Info("TimeWorkOfActionFilter: " + Timer.Elapsed);
        }
    }
}