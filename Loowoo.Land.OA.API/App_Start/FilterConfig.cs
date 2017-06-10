using System.Web;
using System.Web.Mvc;

namespace Loowoo.Land.OA.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
