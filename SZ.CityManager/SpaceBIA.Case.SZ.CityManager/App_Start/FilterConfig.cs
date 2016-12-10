using System.Web;
using System.Web.Mvc;

namespace SpaceBIA.Case.SZ.CityManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
