 /*  作者：       tianzh
 *  创建时间：   2012/7/24 9:26:29
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.UI;
using System.ComponentModel;
using TZHSWEET.IBLL;
using TZHSWEET.BLL;
using TZHSWEET.Common;
using TZHSWEET.ViewModel;

namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
   
    [Description("菜单管理")]
    public class MenusController : BaseController
    {
        #region 菜单管理部分
        //
        // GET: /Admin/Menus/
        [ViewPage]
        [Description("[Index主页]菜单管理")]
        [DefaultPage]
        public ActionResult Index()
        {
            return View();
        }
        [Description("[Index页面Add添加请求]添加菜单")]
        [LigerUIExceptionResult]
        public ActionResult Add()
        {
            MenusRequest request = new MenusRequest(HttpContext);
            IModuleService moduleService = new ModuleService();

            //设置模块路径
            request.Module.ModuleLinkUrl = !request.Controller.IsNullOrEmpty() ?
                ConfigSettings.GetAllController()
                .Where(p => p.ControllerName == request.Controller)
                .SingleOrDefault().LinkUrl : "";
            bool status = moduleService.Insert(request.Module);
            UserOperateLog.WriteOperateLog("[添加菜单操作]" + SysOperate.Operate.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Add);

        }
        [Description("[Index页面Update更新请求]更新菜单")]
        [LigerUIExceptionResult]
        public ActionResult Update()
        {
            MenusRequest request = new MenusRequest(HttpContext);
            IModuleService moduleService = new ModuleService();
            //设置模块路径
            request.Module.ModuleLinkUrl = !request.Controller.IsNullOrEmpty() ?
                ConfigSettings.GetAllController()
                .Where(p => p.ControllerName == request.Controller)
                .SingleOrDefault().LinkUrl : "";
            bool status = moduleService.Update(request.Module);
            UserOperateLog.WriteOperateLog("[修改菜单操作]" + SysOperate.Update.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Update);

        }

        [Description("[Index页面Delete删除请求]删除")]
        [LigerUIExceptionResult]
        public ActionResult Delete()
        {
            MenusRequest request = new MenusRequest(HttpContext);
            IModuleService moduleService = new ModuleService();
            //设置删除状态
            request.Module.IsDeleted = true;
            bool status = moduleService.Update(request.Module);
            UserOperateLog.WriteOperateLog("[删除(假删)菜单操作]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
        [Description("[Index页面永久删除请求]永久删除")]
        [LigerUIExceptionResult]
        public ActionResult RealDelete()
        {
            MenusRequest request = new MenusRequest(HttpContext);
            IModuleService moduleService = new ModuleService();
            bool status = moduleService.Delete(request.Module.ModuleID);
            UserOperateLog.WriteOperateLog("[删除(永久删除)菜单操作]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
        [Description("[SelectController页面加载控制器请求]获取所有的控制器信息(Add,Update必须)")]
        [LoginAllowView]
        public ActionResult GetAllController()
        {
            LigerUIGridRequest gridRequest = new LigerUIGridRequest(HttpContext);
            var content = new ContentResult();
            var data = ConfigSettings.GetAllController();
            //忽略掉公共页面的控制器
            return this.JsonFormat(
                new LigerUIGrid()
                {
                    Rows = LINQHelper.GetIenumberable<MVCController>(data, p => p.ControllerName.ToLower()!="manage",
                    q => gridRequest.SortOrder, gridRequest.PageSize,
                    gridRequest.PageNumber),
                    Total = data.Count
                }
          );
        }
        [Description("[SelectController主页]显示所有的控制器菜单页(Add,Update必须)")]
        [ViewPage]
        [LoginAllowView]
        public ActionResult SelectController()
        {
            return View();
        }
        [Description("[Index页面获得Tree一级树请求]获得一级菜单树(首页必须)")]
        [LoginAllowView]
        public ActionResult GetParentMenuTree()
        {
            //构造分页参数
            LigerUITreeRequest request = new LigerUITreeRequest(HttpContext);
            IUserService userService = new UserService();

            return this.JsonFormat(
                userService.GetUserTreeMenus(request, new SysCurrentUser().UserRoles)
                );

        }
        [Description("[Index页面Grid请求]获取菜单下的子菜单的Grid信息(首页必须)")]
        [LoginAllowView]
        public ActionResult GetMenusGrid()
        {
            //构造分页参数
            LigerUIGridRequest request = new LigerUIGridRequest(HttpContext);
            var result = new ContentResult();
            IUserService userService = new UserService();

            return this.JsonFormat(
                userService.GetUserGridMenus(request, new SysCurrentUser().UserRoles, ConfigSettings.GetAdminUserRoleID().ToString())
                );

        } 
        #endregion

        #region 菜单按钮管理
        [Description("[Index页面跳转]菜单按钮管理")]
        [LigerUIExceptionResult]
        public ActionResult MenuButtonConfig()
        {
            ViewSelectParamPage page=new ViewSelectParamPage (HttpContext);
            IModuleService moduleService=new ModuleService();
          var data=moduleService.GetEntity(p => p.ModuleController == page.ControllerName);
            return this.JsonFormatSuccess(data.ModuleController,"允许访问");

        }
        #endregion
    }
}
