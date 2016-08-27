 /*  作者：       tianzh
 *  创建时间：   2012/8/5 19:02:52
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TZHSWEET.Common;

namespace TZHSWEET.UI
{
    /// <summary>
    /// 拦截Action的异常，输出Json给EXT捕获(目前loaddata类操作在JS中暂时没有处理)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ExceFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LigerUIExceptionResultAttribute), true);
                if (attrs.Length == 1)//判断是否属于LigerUIResult的Action
                {
                    string msgTmp= "<b>异常消息:  </b>{0}</p><b>触发Action:  </b>{1}</p><b>异常类型:  </b>{2}";
                    var excResult = new JsonResult();
                    excResult.Data = AjaxResult.Error(string.Format(msgTmp,
                                filterContext.Exception.GetBaseException().Message,
                                filterContext.ActionDescriptor.ActionName,
                                filterContext.Exception.GetBaseException().GetType().ToString()));
                    LogHelper.WriteLog("系统错误:" + excResult.Data);
                    filterContext.Result = excResult;
                }
                else
                {
                    filterContext.Controller.ViewData["ErrorMessage"] = String.Format(@"<b>异常消息: {0}</br><b>触发Action:  </p>{1}</br><b>异常类型:  </b>{2}",
                        filterContext.Exception.GetBaseException().Message,
                        filterContext.ActionDescriptor.ActionName,
                        filterContext.Exception.GetBaseException().GetType().ToString());
                    LogHelper.WriteLog("系统错误:" + filterContext.Controller.ViewData["ErrorMessage"].ToString ());
                    filterContext.Result = new ViewResult()
                   {
                       ViewName = "Error",
                       ViewData = filterContext.Controller.ViewData,
                   };

                }
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
