/*  作者：       tianzh
*  创建时间：   2012/7/20 8:58:02
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.Common;
using TZHSWEET.BLL;

namespace TZHSWEET.UI
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 权限拦截
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //权限拦截是否忽略
            bool IsIgnored = false;
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var path = filterContext.HttpContext.Request.Path.ToLower();
            //获取当前配置保存起来的允许页面
            IList<string> allowPages = ConfigSettings.GetAllAllowPage();
            foreach (string page in allowPages)
            {
                if (page.ToLower() == path)
                {
                    IsIgnored = true;
                    break;
                }
            }
            if (IsIgnored)
                return;
            //接下来进行权限拦截与验证
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewPageAttribute), true);
            var isViewPage = attrs.Length == 1;//当前Action请求是否为具体的功能页

            if (this.AuthorizeCore(filterContext) == false)//根据验证判断进行处理
            {
                //注：如果未登录直接在URL输入功能权限地址提示不是很友好；如果登录后输入未维护的功能权限地址，那么也可以访问，这个可能会有安全问题
                if (isViewPage == true)
                {
                    //跳转到登录页面
                    filterContext.RequestContext.HttpContext.Response.Redirect("~/Admin/Manage/UserLogin");
                }
                else
                {
                    object[] attrsUIException = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LigerUIExceptionResultAttribute), true);
                    if (attrsUIException.Length == 1)
                    {
                        filterContext.Result = new FormatJsonResult() { IsError=true, Data=null,Message="您没有权限执行此操作！" };//功能权限弹出提示框
                    }
                    else
                        
                        filterContext.RequestContext.HttpContext.Response.Redirect("~/Admin/Manage/Error");
                }
            }
        }
        /// <summary>
        /// [Anonymous标记]验证是否匿名访问
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckAnonymous(ActionExecutingContext filterContext)
        {
            //验证是否是匿名访问的Action
            object[] attrsAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AnonymousAttribute), true);
            //是否是Anonymous
            var Anonymous = attrsAnonymous.Length == 1;
            return Anonymous;
        }
        /// <summary>
        /// [LoginAllowView标记]验证是否登录就可以访问(如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了)
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckLoginAllowView(ActionExecutingContext filterContext)
        {
            //在这里允许一种情况,如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LoginAllowViewAttribute), true);
            //是否是LoginAllowView
            var ViewMethod = attrs.Length == 1;
            return ViewMethod;
        }

        /// <summary>
        /// //权限判断业务逻辑
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="isViewPage">是否是页面</param>
        /// <returns></returns>
        protected virtual bool AuthorizeCore(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            //验证当前Action是否是匿名访问Action
            if (CheckAnonymous(filterContext))
                return true;
            //未登录验证
            if (SessionHelper.Get("UserID") == null)
            {
                return false;
            }
            //验证当前Action是否是登录就可以访问的Action
            if (CheckLoginAllowView(filterContext))
                return true;

            //下面开始用户权限验证
            var user = new UserService();
            SysCurrentUser CurrentUser = new SysCurrentUser();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            //如果是超级管理员,直接允许
            if (CurrentUser.UserID == ConfigSettings.GetAdminUserID())
            {
                return true;
            }
            //如果拥有超级管理员的角色就默认全部允许
            string AdminUserRoleID = ConfigSettings.GetAdminUserRoleID().ToString();
            //检查当前角色组有没有超级角色
            if (Tools.CheckStringHasValue(CurrentUser.UserRoles, ',', AdminUserRoleID))
            {
                return true;
            }

            //Action权限验证
            if (controllerName.ToLower() != "manage")//如果当前Action请求为具体的功能页并且不是Manage中 Index页和Welcome页
            {
                //验证
                if (!user.RoleHasOperatePermission(CurrentUser.UserRoles, controllerName, actionName))//如果验证该操作是否拥有权限
                {
                    return false;
                }
            }
            //管理页面直接允许
            return true;
        }
    }
}