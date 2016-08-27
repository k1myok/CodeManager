using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TZHSWEET.Common;
namespace TZHSWEET.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //配置日志,读取配置信息
            LogHelper.SetConfig();

        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            //记录错误日志
            Exception objExp = HttpContext.Current.Server.GetLastError();
            LogHelper.WriteLog("\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, objExp);
        }
    }
}