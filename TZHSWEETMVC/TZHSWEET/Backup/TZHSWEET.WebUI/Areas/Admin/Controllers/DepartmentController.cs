 /*  作者：       tianzh
 *  创建时间：   2012/7/17 21:34:58
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.BLL;
using TZHSWEET.IBLL;
using TZHSWEET.ViewModel;
using TZHSWEET.UI;
using TZHSWEET.Common;
using System.ComponentModel;
using TZHSWEET.Entity;

namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
   
    [Description("部门管理")]
    public class DepartmentController : BaseController
    {
        /// <summary>
        /// 获取部门GridTree树信息
        /// </summary>
        /// <returns></returns>
        [Description("[Index页面GridTree Ajax请求]获取部门GridTree树信息(主页必须)")]
        [LoginAllowView]
        public ActionResult GetDeptGridTree()
        {
            //构造分页参数
            LigerUIGridRequest gridRequest = new LigerUIGridRequest(HttpContext);
            //没有必要分页,设置每页显示5000个
            gridRequest.PageSize = 5000;
            IDepartmentService departService = new DepartmentService();
             return  this.Content(
                 departService.GetDepartmentGridTree(gridRequest)
                 );
        }
        /// <summary>
        /// 获取部门Select信息
        /// </summary>
        /// <returns></returns>
        [Description("[Detail页面Select Ajax请求]获取部门的下拉列表框(Detail,Update,Add必须)")]
        [LoginAllowView]
        public ActionResult GetDeptSelect()
        {
             LigerUISelectRequest request = new LigerUISelectRequest(HttpContext);
            IDepartmentService departService = new DepartmentService();
            return  this.JsonFormat(
                departService.GetAllDepartmentSelectToViewModel(request)
                );

        }
        //获取当前部门信息
        [Description("[Detail页面加载表单请求]获取一条部门信息(Detail,Update必须)")]
        [LigerUIExceptionResult]
        [LoginAllowView]
        public ActionResult Get()
        {
            //获取请求信息
            DepartmentRequest departRequest = new DepartmentRequest(HttpContext);
            IDepartmentService departService = new DepartmentService();
            //执行状态
            tbDepartment department = departService.GetEntity(p => p.DeptID == departRequest.Department.DeptID);

            //转化为视图UI层的实体对象
            var data = ViewModelDeptment.ToViewModel(department);
            UserOperateLog.WriteOperateLog("获取[部门信息]" + SysOperate.Operate.ToMessage(data.IsNullOrEmpty()));
            return this.JsonFormat(data, SysOperate.Operate);
        }
        //
        // GET: /Admin/Department/
        [Description("[Index主页]部门管理")]
        [DefaultPage]
        [ViewPage]

        public ActionResult Index()
        {
            UserOperateLog.WriteOperateLog("浏览[部门管理]" + SysOperate.Operate.ToMessage(true));
            return View();
        }
        //添加部门]
        [Description("[Index页面Add请求]添加部门")]
        [LigerUIExceptionResult]
        public ActionResult Add()
        {
            SysCurrentUser user=new SysCurrentUser ();
            //获取请求信息
            DepartmentRequest Request = new DepartmentRequest(HttpContext, user.UserID, false);
            IDepartmentService departService = new DepartmentService();
            var data = departService.Insert(Request.Department);
            UserOperateLog.WriteOperateLog("[部门信息]" + SysOperate.Add.ToMessage(data.IsNullOrEmpty()));
            return this.JsonFormat(data, SysOperate.Operate);
        }
        //更新部门信息
        [Description("[Index页面Update请求]更新部门")]
        [LigerUIExceptionResult]
        public ActionResult Update()
        {
            SysCurrentUser user = new SysCurrentUser();
            //获取请求信息
            DepartmentRequest Request = new DepartmentRequest(HttpContext, user.UserID, true);
            IDepartmentService departService = new DepartmentService();
            var data = departService.Update(Request.Department);
            UserOperateLog.WriteOperateLog("[部门信息]" + SysOperate.Update.ToMessage(data.IsNullOrEmpty()));
            return this.JsonFormat(data, SysOperate.Update);
        }
        //删除
        [Description("[Index页面Delete请求]删除(假删)部门")]
        [LigerUIExceptionResult]
        public ActionResult Delete()
        {
            //获取请求信息
            DepartmentRequest Request = new DepartmentRequest(HttpContext);
            IDepartmentService departService = new DepartmentService();
            Request.Department.IsDeleted = true;
            bool  status = departService.Update(Request.Department);
            UserOperateLog.WriteOperateLog("[部门信息]删除(假删)部门:" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
        //删除
        [Description("[Index页面Delete请求]永久删除部门")]
        [LigerUIExceptionResult]
        public ActionResult RealDelete()
        {
            //获取请求信息
            DepartmentRequest Request = new DepartmentRequest(HttpContext);
            IDepartmentService departService = new DepartmentService();
            var status = departService.Delete(Request.Department.DeptID);
            UserOperateLog.WriteOperateLog("[部门信息]永久删除" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
        [Description("部门树级下拉框请求")]
        [LoginAllowView]
        public ActionResult GetDepartmentTree()
        {
            LigerUITreeRequest treeRequest = new LigerUITreeRequest(HttpContext);
            IDepartmentService departService = new DepartmentService();
             return this.JsonFormat(departService.GetDepartmentTree(treeRequest));
        }

         //查看部门
        [Description("[Detail主页]浏览部门详细信息")]
        [ViewPage]
        public ActionResult Detail()
        {
            ViewDetailPage page = new ViewDetailPage(HttpContext);
            ViewBag.IsView = page.IsView;
            ViewBag.CurrentID = page.CurrentID;
            UserOperateLog.WriteOperateLog("[浏览部门详细信息]" + SysOperate.Load.ToMessage(true));
            return View();
        }

    }
}
