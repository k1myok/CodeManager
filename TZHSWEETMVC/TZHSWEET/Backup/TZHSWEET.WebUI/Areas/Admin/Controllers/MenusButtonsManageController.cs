 /*  作者：       tianzh
 *  创建时间：   2012/7/27 15:26:02
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using TZHSWEET.UI;
using TZHSWEET.ViewModel;
using TZHSWEET.IBLL;
using TZHSWEET.BLL;
using TZHSWEET.Common;

namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
    
    public class MenusButtonsManageController : BaseController
    {
        //
        // GET: /Admin/MenusButtons/
        [Description("[菜单按钮管理主页]")]
        [ViewPage]
        [DefaultPage]
        public ActionResult Index()
        {
            ViewSelectParamPage select = new ViewSelectParamPage(HttpContext);
            ViewBag.ControllerName = select.ControllerName;
            ViewBag.MenuID = select.MenuID;
            return View();
        }
        [Description("[菜单按钮管理主页Grid 加载的ajax请求]根据菜单获取按钮(首页必须)")]
        [LoginAllowView]
        public ActionResult GetMenuButtons()
        {
            LigerUIGridRequest request=new LigerUIGridRequest (HttpContext);
            IModulePermissionService modulePermissionService = new ModulePermissionService();
        
            return this.JsonFormat(
                modulePermissionService.GetMenuButtons(request)
                );
        }
        [Description("[菜单按钮管理主页]页面跳转选择按钮页面(Add,Update必须)")]
        [LoginAllowView]
        public ActionResult SelectButtons()
        {
            ViewSelectParamPage select = new ViewSelectParamPage(HttpContext);
            ViewBag.ControllerName = select.ControllerName;
            return View();
        }
        [Description("[菜单按钮管理主页]加载数据库权限表信息(Add,Update必须)")]
        [LoginAllowView]
        public ActionResult SelectPermission()
        {
            LigerUIGridRequest request = new LigerUIGridRequest(HttpContext);
            IPermissionService permissionService = new PermissionService();
            return this.JsonFormat(permissionService.GetPermissionForGrid(request));
        }
        [Description("[菜单按钮管理主页]添加请求")]
        [LigerUIExceptionResult]
        public ActionResult Add(){

            ViewModelMenuButtons requestButton = new ViewModelMenuButtons(HttpContext,true);
            IModulePermissionService modulePermission = new ModulePermissionService();
            bool status = modulePermission.Insert(requestButton);
            UserOperateLog.WriteOperateLog("[菜单按钮权限]" + SysOperate.Add.ToMessage(status));
            return this.JsonFormat(status,status,SysOperate.Add);
          
        }
        [Description("[菜单按钮管理主页]修改请求")]
        [LigerUIExceptionResult]
        public ActionResult Update()
        {

            ViewModelMenuButtons requestButton = new ViewModelMenuButtons(HttpContext, false);
            IModulePermissionService modulePermission = new ModulePermissionService();
            bool status = modulePermission.Update(requestButton);
            UserOperateLog.WriteOperateLog("[菜单按钮权限]" + SysOperate.Update.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Update);

        }
        [Description("[菜单按钮管理主页]删除(假删除)")]
        [LigerUIExceptionResult]
        public ActionResult Delete()
        { 
            ViewModelMenuButtons requestButton = new ViewModelMenuButtons(HttpContext, false);
            IModulePermissionService modulePermission = new ModulePermissionService();
            requestButton.IsDeleted = true;
            bool status = modulePermission.Update(requestButton);
            UserOperateLog.WriteOperateLog("[菜单按钮权限假删除]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
        [Description("[菜单按钮管理主页]永久删除动作")]
        [LigerUIExceptionResult]
        public ActionResult RealDelete()
        {
            ViewModelMenuButtons requestButton = new ViewModelMenuButtons(HttpContext, false);
            IModulePermissionService modulePermission = new ModulePermissionService();
            bool status = modulePermission.Delete(requestButton.ModulePermissionID);
            UserOperateLog.WriteOperateLog("[(永久)删除菜单按钮权限信息]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);

        }

        
    }
}
