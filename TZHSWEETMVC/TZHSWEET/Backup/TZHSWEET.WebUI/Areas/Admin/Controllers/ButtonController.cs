 /*  作者：       tianzh
 *  创建时间：   2012/7/26 15:32:27
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.ViewModel;
using TZHSWEET.IBLL;
using TZHSWEET.BLL;
using TZHSWEET.Common;
using TZHSWEET.UI;
using System.ComponentModel;
namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
  
    public class ButtonController : BaseController
    {
        //
        // GET: /Admin/Permission/
        [Description("[系统权限维护页]按钮管理")]
        [DefaultPage]
        [ViewPage]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 请求权限功能列表信息
        /// </summary>
        /// <returns></returns>
        [Description("[系统权限维护页Ajax请求]请求权限功能列表信息(返回Grid)(主页必须)")]
        [LoginAllowView]
        public ActionResult GetPermissionForGrid()
        {
            LigerUIGridRequest request = new LigerUIGridRequest(HttpContext);
            //GridTree禁用分页,设置每页5000条
            request.PageSize = 5000;
            IPermissionService permissionService = new PermissionService();
             return  this.JsonFormat(permissionService.GetPermissionGridTree(request));

        }
        [Description("[选择动作页面]请求动作信息页面(Add和Update必须)")]
        [LoginAllowView]
        [ViewPage]
        public ActionResult SelectAction()
        {
            ViewSelectParamPage select = new ViewSelectParamPage(HttpContext);
            ViewBag.ControllerName = select.ControllerName;
            return View();
        }
        [Description("[选择动作页面Grid Ajax请求]请求所有控制器的动作信息(Add和Update必须)")]
        [LoginAllowView]
        public ActionResult GetAllAction()
        {
            LigerUIGridRequest gridRequest = new LigerUIGridRequest(HttpContext);
            var data = ConfigSettings.GetAllAction();
            //忽略掉公共页面的权限动作
            return this.JsonFormat(
                new LigerUIGrid()
                {
                    Rows = LINQHelper.GetIenumberable<MVCAction>(data,
                           p => p.ControllerName.ToLower()!="manage",
                           q => gridRequest.SortOrder, gridRequest.PageSize,
                           gridRequest.PageNumber),
                    Total = data.Count()
                });
            
        }
        [Description("[系统权限维护页]添加动作")]
        [LigerUIExceptionResult]
        public ActionResult Add()
        {
            ViewModelButton viewModel = new ViewModelButton(HttpContext);
            viewModel.Description =
                !viewModel.Action.IsNullOrEmpty() && !viewModel.Controller.IsNullOrEmpty() ? ConfigSettings.GetAllAction()
                .Where(p => p.ActionName == viewModel.Action && p.ControllerName == viewModel.Controller)
                .SingleOrDefault()
                .Description
                : "";
            IPermissionService permissionService = new PermissionService();
            bool status = permissionService.Insert(ViewModelButton.ToEntity(viewModel));
            UserOperateLog.WriteOperateLog("[添加权限动作信息]" + SysOperate.Add.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Add);

        }
        [Description("[系统权限维护页]修改动作")]
        [LigerUIExceptionResult]
        public ActionResult Update()
        {
                ViewModelButton viewModel = new ViewModelButton(HttpContext);
                viewModel.Description =
                  !viewModel.Action.IsNullOrEmpty() && !viewModel.Controller.IsNullOrEmpty() ?
                   ConfigSettings.GetAllAction()
                  .Where(p => p.ActionName == viewModel.Action && p.ControllerName == viewModel.Controller)
                  .SingleOrDefault()
                  .Description
                  : "";
                IPermissionService permissionService = new PermissionService();
                bool status = permissionService.Update(ViewModelButton.ToEntity(viewModel));
                UserOperateLog.WriteOperateLog("[修改权限动作信息]" + SysOperate.Update.ToMessage(status));
                return this.JsonFormat(status, status, SysOperate.Update);

        }

        [Description("[系统权限维护页]软删除动作")]
        [LigerUIExceptionResult]
        public ActionResult Delete()
        {
            ViewModelButton viewModel = new ViewModelButton(HttpContext);
            viewModel.IsVisible = false;
            IPermissionService permissionService = new PermissionService();
            bool status = permissionService.Update(ViewModelButton.ToEntity(viewModel));
            UserOperateLog.WriteOperateLog("[(软)删除权限动作信息]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);

        }
        [Description("[系统权限维护页]永久删除动作")]
        [LigerUIExceptionResult]
        public ActionResult RealDelete()
        {
            ViewModelButton viewModel = new ViewModelButton(HttpContext);
            IPermissionService permissionService = new PermissionService();
            bool status = permissionService.Delete(viewModel.ID);
            UserOperateLog.WriteOperateLog("[(永久)删除权限动作信息]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);

        }


    }
}
