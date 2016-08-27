 /*  作者：       tianzh
 *  创建时间：   2012/7/19 10:36:31
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.IBLL;
using TZHSWEET.Entity;
using TZHSWEET.Common;
using TZHSWEET.BLL;
using TZHSWEET.UI;
using System.ComponentModel;
using TZHSWEET.ViewModel;

namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
    
    [Description("用户管理")]
    public class UserController : BaseController
    {

        [Description("[用户管理]获取用户信息(主页必须)")]
        [LoginAllowView]
        public ActionResult GetUserForGrid()
        {
            LigerUIGridRequest requestGrid = new LigerUIGridRequest(HttpContext);
            IUserService roleService = new UserService();
            return this.JsonFormat(roleService.GetAllUsers(requestGrid));
        }
        //获取当前部门信息
        [Description("[Detail页面加载表单请求]获取一条用户信息(Detail,Update必须)")]
        [LigerUIExceptionResult]
        [LoginAllowView]
        public ActionResult Get()
        {
            //获取请求信息
            //ViewModelUser userRequest = new ViewModelUser(HttpContext,false);
            RequestBase userRequest = new RequestBase(HttpContext);
            IUserService userService = new UserService();
            //执行状态
            tbUser user = userService.GetEntity(p => p.UserID == userRequest.ID.ObjToInt());

            //转化为视图UI层的实体对象
            var data = ViewModelUser.ToViewModel(user);
             data.RoleIDs=userService.GetUserAllRole(data.UserID);
            return this.JsonFormat(data,!data.IsNullOrEmpty(),SysOperate.Operate);
        }
        [DefaultPage]
        [Description("[用户管理首页]列表信息")]
        public ActionResult Index()
        {
            return View();
        }

        [ViewPage]
        [Description("[用户详细信息]详细信息(add,Update,Detail必备)")]
        public ActionResult Detail()
        {
            ViewDetailPage page = new ViewDetailPage(HttpContext);
            ViewBag.IsView = page.IsView;
            ViewBag.CurrentID = page.CurrentID;
            UserOperateLog.WriteOperateLog("[浏览用户详细信息]" + SysOperate.Load.ToMessage(true));
            return View();
         
        }
        [LoginAllowView]
        [Description("[用户详细信息]检查帐号名是否存在(add,Update必备)")]
        public ActionResult CheckUserNameExist()
        {
            UserRequest request = new UserRequest (HttpContext,true);
            IUserService userService = new UserService();
            return Content((userService.GetCount(p=>p.UserName==request.UserName)==0).ToString ().ToLower());
        }
         [Description("[用户详细信息]添加用户")]
         [LigerUIExceptionResult]
        public ActionResult Add()
        {
            ViewModelUser user = new ViewModelUser(HttpContext,true);
            IUserService userService = new UserService();
            bool status= userService.Insert(ViewModelUser.ToEntity(user),user.RoleIDs);
            UserOperateLog.WriteOperateLog("[添加用户]" + SysOperate.Add.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Add);
        }
         [Description("[用户详细信息]更新用户")]
         [LigerUIExceptionResult]
        public ActionResult Update()
        {
            ViewModelUser user = new ViewModelUser(HttpContext, false);
            IUserService userService = new UserService();
               bool status=false;
               if (user.UserID > 0)
               {
                   status = userService.Update(ViewModelUser.ToEntity(user), user.RoleIDs);
                   UserOperateLog.WriteOperateLog("[修改用户]" + SysOperate.Update.ToMessage(status));
               }
              return this.JsonFormat(status, status, SysOperate.Update);
        }
         [Description("[用户详细信息]删除用户")]
         [LigerUIExceptionResult]
        public ActionResult Delete()
        {
            ViewDetailPage page = new ViewDetailPage(HttpContext);
            IUserService userService = new UserService();
            var data = userService.GetEntity(p => p.UserID == Convert.ToInt32(page.CurrentID));
            data.IsDeleted = true;
            bool status = userService.Update(data);
            UserOperateLog.WriteOperateLog("[删除用户]假删:" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Delete);
        }
         [Description("[用户详细信息]永久删除用户")]
         [LigerUIExceptionResult]
         public ActionResult RealDelete()
         {
             ViewDetailPage page = new ViewDetailPage(HttpContext);
             IUserService userService = new UserService();
             bool status = userService.Delete(page.CurrentID);
             UserOperateLog.WriteOperateLog("[删除用户]永久删除:" + SysOperate.Delete.ToMessage(status));
             return this.JsonFormat(status, status, SysOperate.Delete);
         }
    }
}
