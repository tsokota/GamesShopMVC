using System.Web.Mvc;
using Yevhenii_KoliesnikTask1.Filters;

namespace Yevhenii_KoliesnikTask1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new IPRequestGlobalFilter());
        }
    }
}