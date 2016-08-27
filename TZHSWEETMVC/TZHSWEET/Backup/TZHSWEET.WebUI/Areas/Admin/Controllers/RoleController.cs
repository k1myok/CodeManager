/*  作者：       tianzh
*  创建时间：   2012/7/30 15:30:17
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
using TZHSWEET.ViewModel;
using TZHSWEET.Common;
namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        //
        // GET: /Admin/Role/
        [DefaultPage]
        [Description("[角色管理首页]")]
        public ActionResult Index()
        {
            return View();
        }
        [Description("[角色管理首页 Grid请求]Ajax获取所有角色数据(首页必须)")]
        [LoginAllowView]
        public ActionResult GetAllRolesForGrid()
        {
            LigerUIGridRequest requestGrid = new LigerUIGridRequest(HttpContext);
            IRoleService roleService = new RoleService();
            return this.JsonFormat(roleService.GetAllRoles(requestGrid));
        }
        [Description("角色选择的下拉框")]
        [LoginAllowView]
        public ActionResult GetRolesForSelect()
        {
            LigerUISelectRequest requestSelect = new LigerUISelectRequest(HttpContext);
            IRoleService roleService = new RoleService();
            return this.JsonFormat(roleService.GetRolesForSelect(requestSelect));
        }
        [Description("角色树形选择")]
        [LoginAllowView]
        public ActionResult GetRolesForTree()
        {
            LigerUITreeRequest requestTree = new LigerUITreeRequest(HttpContext);
            IRoleService roleService = new RoleService();
          return this.JsonFormat(roleService.GetRoleTree(requestTree));
        }

        [Description("[角色管理首页]添加角色")]
        [LigerUIExceptionResult]
        public ActionResult Add()
        {
            ViewModelRole request = new ViewModelRole(HttpContext, true);
            IRoleService roleService = new RoleService();
            bool status = roleService.Insert(ViewModelRole.ToEntity(request));
            UserOperateLog.WriteOperateLog("[添加角色操作]" + SysOperate.Add.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Add);
        }
        [Description("[角色管理首页]修改角色")]
        [LigerUIExceptionResult]
        public ActionResult Update()
        {
            ViewModelRole request = new ViewModelRole(HttpContext, false);
            IRoleService roleService = new RoleService();
            bool status = roleService.Update(ViewModelRole.ToEntity(request));
            UserOperateLog.WriteOperateLog("[修改角色操作]" + SysOperate.Update.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Update);
        }
        [Description("[角色管理首页]删除(假删)角色")]
        [LigerUIExceptionResult]
        public ActionResult Delete()
        {
            ViewModelRole request = new ViewModelRole(HttpContext, false);
            IRoleService roleService = new RoleService();
            request.IsDeleted = true;
            bool status = roleService.Update(ViewModelRole.ToEntity(request));
            UserOperateLog.WriteOperateLog("[删除(假删)角色操作]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
        [Description("[角色管理首页]永久删除角色")]
        [LigerUIExceptionResult]
        public ActionResult RealDelete()
        {
            ViewModelRole request = new ViewModelRole(HttpContext, false);
            IRoleService roleService = new RoleService();
            bool status = roleService.Delete(request.RoleID);
            UserOperateLog.WriteOperateLog("[永久删除角色操作]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
    }
}
