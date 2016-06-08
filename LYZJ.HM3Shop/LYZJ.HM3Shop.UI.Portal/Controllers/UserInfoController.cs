
using LYZJ.HM3Shop.BLL;
using LYZJ.HM3Shop.IBLL;
using LYZJ.HM3Shop.Model;
using LYZJ.HM3Shop.Model.Enum;
using LYZJ.HM3Shop.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LYZJ.HM3Shop.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        //实例化需要的对象
        IUserInfoService _userInfoService = new UserInfoService();
        IRoleService _roleService = new RoleService();
        IR_UserInfo_RoleService _userInfoRoleService = new R_UserInfo_RoleService();
        IR_UserInfo_ActionInfoService _userActionInfoService = new R_UserInfo_ActionInfoService();
        IActionInfoService _actionInfo = new ActionInfoService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 得到用户的所有信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUserInfos()
        {
            //json数据：{total:"",row:""}
            //
            //分页的数据
            //

            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);


            ////SearchName,SearchMail
            string searchName = Request["SearchName"];
            string searchMail = Request["SearchMail"];

            //封装一个业务逻辑层，来处理分页过滤的事件
            GetModelQuery userInfoQuery = new GetModelQuery();
            userInfoQuery.pageIndex = pageIndex;
            userInfoQuery.pageSize = pageSize;
            userInfoQuery.Name = searchName;
            userInfoQuery.Mail = searchMail;
            userInfoQuery.total = 0;

            //放置依赖刷新
            var data = from u in _userInfoService.LoadSearchData(userInfoQuery)
                       select new { u.ID, u.UName, u.Phone, u.Pwd, u.Mail, u.LastModifiedOn, u.SubTime };

            //var data = _userInfoService.LoadPagerEntities(pageSize, pageIndex, out total, u => true, true, u => u.ID);

            var result = new { total = userInfoQuery.total, rows = data };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public ActionResult Regist(UserInfo userinfo)
        {
            //给表中的默认字段赋值
            userinfo.LastModifiedOn = DateTime.Now;
            userinfo.SubTime = DateTime.Now;
            //在这里需要用到枚举类型，不要写0
            userinfo.DelFlag = (short)DelFlagEnum.Normal;

            _userInfoService.AddEntities(userinfo);
            return Content("OK");

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="deleteUserInfoID"></param>
        /// <returns></returns>
        public ActionResult DeleteUserInfo(string deleteUserInfoID, string UName)
        {
            //首先确认是哪个用户登录进来的，如果此用户正在登录系统，则不允许删除此用户
            UserInfo UInfo = Session["UserInfo"] as UserInfo;
            var LoginUName = UInfo.UName;

            var UIdsName = UName.Split(',');
            List<string> deleteUName = new List<string>();
            foreach (var Name in UIdsName)
            {
                deleteUName.Add(Name);
            }
            if (deleteUName.Contains(LoginUName))
            {
                return Content("含有正在使用的用户，禁止删除");
            }

            if (string.IsNullOrEmpty(deleteUserInfoID))
            {
                return Content("请选择您要删除的数据");
            }
            //截取传递过来的字符串的数字信息
            var idsStr = deleteUserInfoID.Split(',');

            List<int> deleteIDList = new List<int>();

            foreach (var ID in idsStr)
            {
                deleteIDList.Add(Convert.ToInt32(ID));
            }
            if (_userInfoService.DeleteUserInfo(deleteIDList) > 0)
            {
                return Content("OK");
            }
            return Content("删除失败，请您检查");
            

            #region -----实现只删除一条数据--------
                //_userInfoService.DeleteUser(deleteIDList);
                //实例化UserInfo表
                //UserInfo userInfo = new UserInfo();
                //userInfo.ID = deleteUserInfoID;
                //_userInfoService.DeleteEntities(userInfo);
            #endregion

        }

        /// <summary>
        /// 绑定用户数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult GetBindDetails(int ID)
        {
            var BindIDForUpdateTextBox = _userInfoService.LoadEntities(u => u.ID == ID).FirstOrDefault();

            //JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
            //var Details = JavaScriptSerializer.Serialize(BindIDForUpdateTextBox);
            //return Content(Details);
            return Json(BindIDForUpdateTextBox, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult UpdateInfo(UserInfo userInfo)
        {
            //首先查询出要修改的实体对象
            var EditUserInfo = _userInfoService.LoadEntities(c => c.ID == userInfo.ID).FirstOrDefault();

            //查询出实体对象给重新复制
            EditUserInfo.UName = userInfo.UName;
            EditUserInfo.Pwd = userInfo.Pwd;
            EditUserInfo.Mail = userInfo.Mail;
            EditUserInfo.Phone = userInfo.Phone;
            _userInfoService.UpdateEntities(userInfo);
            return Content("OK");
        }

        /// <summary>
        /// 检查用户名是否已经存在
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult CheckUserName(UserInfo userInfo)
        {
            var UName = _userInfoService.LoadEntities(u => u.UName == userInfo.UName).FirstOrDefault();
            if (UName != null)
            {
                return Content("OK");
            }
            else
            {
                return Content("Error");
            }
        }

        /// <summary>
        /// 设置用户角色,get提交数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SetRole(int ID)
        {
            //查询出当前的用户信息
            var currentSetRoleUser = _userInfoService.LoadEntities(c => c.ID == ID).FirstOrDefault();
            
            //做成一个强类型，传递给前台接收
            ViewData.Model = currentSetRoleUser;

            //前台需要所有的角色的信息
            //注意：在Lambda或者Linq里面不能进行强制类型转换，所以在外面直接转换好
            short deleteNormal=(short)DelFlagEnum.Normal;
            var allRoles = _roleService.LoadEntities(c => c.DelFlag == deleteNormal).ToList();
            //使用ViewBag传递给前台角色信息
            ViewBag.AllRoles = allRoles;

            //在前台传递用户已经关联的角色信息,使用linq查询出来这些信息
            ViewBag.ExtistRoleIds = (from r in currentSetRoleUser.R_UserInfo_Role  //当前用户和角色中间表的数据
                                     select r.RoleID).ToList();

            return View();
        }

        /// <summary>
        /// 处理用户设置的角色，Post请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRole()
        {
            //拿到当前设置角色的用户ID
            int userId = Request["hidUserId"] == null ? 0 : Convert.ToInt32(Request["hidUserId"]);
            //查询出当前用户
            var currentSetUser = _userInfoService.LoadEntities(c => c.ID == userId).FirstOrDefault();
            if (currentSetUser != null)
            {
                //给当前用户设置角色
                //从前台拿到所有的角色，只要选中了表单，就会发送一个ckb_2
                //从请求的表单里面拿到所有的以ckb_开头的Key
                var allKeys = from key in Request.Form.AllKeys
                              where key.StartsWith("ckb_")
                              select key;
                //定义一个list集合存放传递过来的Key
                List<int> roleIds = new List<int>();
                if (userId > 0)
                {
                    //循环遍历出所有的前台表单传递过来的数据的信息
                    foreach (var key in allKeys)
                    {
                        roleIds.Add(Convert.ToInt32(key.Replace("ckb_", "")));
                        #region ----第一回的写法   错误-----
                        ////批量差入，最好添加一个批量插入的方法
                        //R_UserInfo_Role rUserInfoRole = new R_UserInfo_Role();
                        //rUserInfoRole.RoleID = Convert.ToInt32(key.Replace("ckb_", ""));
                        //rUserInfoRole.UserInfoID = userId;
                        //rUserInfoRole.SubTime = DateTime.Now;
                        ////这样写是有错误的，也就是添加的信息添加进去再次选择的时候没有请出原来的数据
                        //_userInfoRoleService.AddEntities(rUserInfoRole); 
                        #endregion
                    }
                }
                _userInfoService.SetUserInfoRole(userId, roleIds);
            }
            return Content("OK");
        }

        /// <summary>
        /// 设置用户高级权限的效果
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult SetUserActionInfo(int ID)
        {
            //返回用户的所有的权限信息在这里显示
            var currentUser = _userInfoService.LoadEntities(c => c.ID == ID).FirstOrDefault();
            ViewData.Model = currentUser;

            //返回用户设置的权限的信心
            ViewBag.ActionData = (from n in currentUser.R_UserInfo_ActionInfo
                                  select new UserSpecialActionInfo()
                                  {
                                      RID = n.ID,
                                      ActionID = n.ActionInfoID,
                                      ActionName = n.ActionInfo.ActionName,
                                      ActionURL = n.ActionInfo.RequestUrl,
                                      HasPermation = n.HasPermation,
                                      UserID = n.UserInfoID
                                  }).ToList();

            return View(currentUser);
        }

        /// <summary>
        /// 设置用户的权限是否可以使用
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePermation()
        {
            //
            int userID = Request["hidenId"] == null ? 0 : Convert.ToInt32(Request["hidenId"]);
            var currentSetUser = _userInfoService.LoadEntities(c => c.ID == userID).FirstOrDefault();
            if (currentSetUser != null)
            {
                var allKeys = from key in Request.Form.AllKeys
                              where key.StartsWith("HasPass_")
                              select key;
                //定义一个集合遍历存取传递过来key
                List<int> actionIDS = new List<int>();
                if (userID > 0)
                {
                    //循环遍历出所有的前台表单传递过来的数据的信息
                    foreach (var key in allKeys)
                    {
                        actionIDS.Add(Convert.ToInt32(key.Replace("HasPass_", "")));
                    }
                }
                //R_UserInfo_ActionInfo ruserInfoService = new R_UserInfo_ActionInfo();
                //ruserInfoService.HasPermation = true;
                //_userActionInfoService.UpdateEntities(ruserInfoService);
                //_userInfoService.SetActionInfoRole(userID, actionIDS);
            }
            return Content("OK");
        }

        /// <summary>
        /// 设置给用户添加特殊权限在页面上面显示
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult AddUserActionInfo(int ID)
        {
            //首先根据用户ID查询到所有的信息
            var userCurrent = _userInfoService.LoadEntities(c => c.ID == ID).FirstOrDefault();
            //封装成一个强类型传递给前台
            ViewData.Model = userCurrent;
            //查询出所有的权限信息显示前台信息
            var allActions = _actionInfo.LoadEntities(c => true).ToList();
            ViewBag.AllActions = allActions;

            //查询出关联表的信息
            ViewBag.Exists = (from r in userCurrent.R_UserInfo_ActionInfo
                              select r.ActionInfoID).ToList();
            return View();
        }

        /// <summary>
        /// 给用户设置特殊权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetActionInfo()
        {
            //首先拿到从前台传递过来的用户ID
            int UserID = Request["hideUserID"] == null ? 0 : Convert.ToInt32(Request["hideUserID"]);
            //根据ID查询出用户的信息
            var currentUser = _userInfoService.LoadEntities(c => c.ID == UserID).FirstOrDefault();
            if (currentUser != null)
            {
                //遍历出前台表单传递过来的含有ckb_的信息，ckb_3，ckb_4
                var allKeys = from key in Request.Form.AllKeys
                              where key.StartsWith("ckb_")
                              select key;
                //定义一个List集合用来存放key
                List<int> list = new List<int>();
                if (UserID > 0)
                {
                    //循环遍历key添加到集合众
                    foreach (var key in allKeys)
                    {
                        list.Add(Convert.ToInt32(key.Replace("ckb_", "")));
                    }
                }
                _userInfoService.SetAddActionInfoRole(UserID, list);
            }
            return Content("OK");
        }

        /// <summary>
        /// 异步删除用户地下的权限信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteUserAction(int ID)
        {
            R_UserInfo_ActionInfo userActionInfo = new R_UserInfo_ActionInfo();
            userActionInfo.ID = ID;
            _userActionInfoService.DeleteEntities(userActionInfo);
            return Content("OK");
        }
    }
}
