 /*  作者：       tianzh
 *  创建时间：   2012/8/3 15:44:01
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using TZHSWEET.UI;
using TZHSWEET.Common;
using TZHSWEET.ViewModel;
using TZHSWEET.BLL;
using TZHSWEET.IBLL;
namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
    public class RolePermissionController : BaseController
    {
        //
        // GET: /Admin/RolePermission/
        [DefaultPage]
        [Description("角色权限管理")]
      
        public ActionResult Index()
        {
            //获取超级管理员角色ID
            ViewBag.AdminRoleID = ConfigSettings.GetAdminUserRoleID().ToString();
            return View();
        }
        
        [Description("获取角色权限Grid数据")]
        [LoginAllowView]
        public ActionResult GetPermissionGrid()
        {
            LigerUIGridRequest request = new LigerUIGridRequest(HttpContext);
            //GridTree禁用分页,设置每页5000条
            request.PageSize = 5000;
            IRolePermissionService permissionService = new RolePermissionService();
            return this.JsonFormat(permissionService.GetPermissionForGridTree(request));

        }
        [Description("授予角色权限")]
        [LigerUIExceptionResult]
        public ActionResult GrantRolePermission()
        {
            GrantRoleRequest request = new GrantRoleRequest(HttpContext);
            IRolePermissionService permissionService = new RolePermissionService();
            bool status=permissionService.GrantRolePermission(request);
            UserOperateLog.WriteOperateLog("[对角色:" + request.RoleID + "]进行权限分配:" + SysOperate.Operate.ToMessage(status));
            return this.JsonFormat(status,status,SysOperate.Operate);
        }
        [Description("获取角色权限数据信息(权限授予的数据")]
        [LigerUIExceptionResult]
        [LoginAllowView]
        public ActionResult GetRolePermissionForData()
        {
            LigerUIGridRequest request = new LigerUIGridRequest(HttpContext);
            IRolePermissionService permissionService = new RolePermissionService();
            return this.JsonFormat(permissionService.GetAllRolePermission(request),SysOperate.Load);
        }
        

    }
}
