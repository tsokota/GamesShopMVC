using System.Web.Mvc;

namespace Yevhenii_KoliesnikTask1.Filters
{
    public class IPRequestGlobalFilter : IActionFilter
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        // Use global filter to log IP of requests in txt file.
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.Info(System.Web.HttpContext.Current.Request.UserHostAddress + "IP some request");
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