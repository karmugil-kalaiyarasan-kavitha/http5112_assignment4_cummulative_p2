using System.Web;
using System.Web.Mvc;

namespace http5112_assignment4_cummulative_p2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
