using System.Web.Mvc;

namespace Yevhenii_KoliesnikTask1.Filters
{
    public class ExceptionControllerAttribute : FilterAttribute, IExceptionFilter
    {

        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    
   

        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                _logger.Error("ErrorException", filterContext.Exception);
                filterContext.Result = new HttpStatusCodeResult(500);
                #if DEBUG
                filterContext.ExceptionHandled = false;
                #else
                filterContext.ExceptionHandled = true;
                #endif
            }
        }
    }
}