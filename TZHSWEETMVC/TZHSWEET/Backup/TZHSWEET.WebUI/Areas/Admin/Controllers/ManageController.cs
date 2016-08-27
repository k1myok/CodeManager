 /*  作者：       tianzh
 *  创建时间：   2012/7/16 15:43:02
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.UI;
using TZHSWEET.IBLL;
using TZHSWEET.BLL;
using TZHSWEET.ViewModel;
using TZHSWEET.Common;
using TZHSWEET.Entity;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;
using System.Drawing.Imaging;

namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
    
    [Description("系统管理")]
    public class ManageController : BaseController
    {
        [Description("获取验证码")]
        [Anonymous]
        public ActionResult GetImgVerifyChars()
        {
            // 在此处放置用户代码以初始化页面
            HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); //不缓存
            Common.YZMHelper yz = new Common.YZMHelper();
            yz.CreateImage();
            Session["CheckCode"] = yz.Text; //将验证字符写入Session，供前台调用
            MemoryStream ms = new MemoryStream();
            yz.Image.Save(ms, ImageFormat.Png);
            yz.Image.Dispose();
            return File(ms.ToArray(), @"image/jpeg");
        }

        //
        // GET: /Admin/Home/
        [Description("[Index首页]系统管理")]
        [DefaultPage]
         [ViewPage]
        public ActionResult Index()
        {
            return View();
        }
        [ViewPage]
        [Anonymous]
        public ActionResult UserLogin()
        {

           // SessionHelper.SetSession("UserID", "1");
           // SessionHelper.SetSession("UserRoles", "1,2,3");

            return View("Login");
        }
        [Anonymous]
        [Description("安全退出")]
        public ActionResult Logout()
        {
            SessionHelper.Del("UserID");
            SessionHelper.Del("UserRoles");
            return this.JsonFormat(true);
        }
        [ViewPage]
        [Anonymous]
        public ActionResult Error()
        {
            return View("Error");

        }
         #region 菜单
         /// <summary>
         /// 获取菜单
         /// </summary>
         /// <returns></returns>
         [Description("[Index首页Load菜单树]获取菜单信息")]
        [LoginAllowView]
         public ActionResult GetMenus()
         {

             IUserService service = new UserService();
             SysCurrentUser CurrentUser = new SysCurrentUser();
             //序列化json数据并返回给前台

              return  this.JsonFormat(
                  service.GetUserPermissionMenus(CurrentUser.UserRoles)
                  );
         } 
         #endregion
       
         #region 按钮
        /// <summary>
        /// 按钮信息加载
        /// </summary>
        /// <returns></returns>
         [Description("所有列表页面的按钮加载")]
         [LoginAllowView]
         
         public ActionResult GetMyButton()
         {
             SysCurrentUser user=new SysCurrentUser ();
             IUserService userService = new UserService();
             //获取请求数据
             ButtonRequest RequestButtonData=new ButtonRequest (HttpContext);

             return this.JsonFormatSuccess(
                 userService.GetButtons(user.UserRoles, 
                 RequestButtonData,
                 ConfigSettings.GetAdminUserRoleID().ToString ()), 
                 SystemMessage.LoadSuccess.ToString ()
                 );
             
            
         }
        [Description("获取所有的按钮图标")]
        [LoginAllowView]
         public ActionResult GetIcons()
         {
             var cache = CacheHelper.GetCache("SystemIcons");
             if (cache != null)
             {
                 return this.JsonFormatSuccess(cache, SystemMessage.LoadSuccess.ToString());
             }
             var rootPath = HttpContext.Server.MapPath("~/Content/icons/");
             string dirPath = rootPath + "32X32\\";
             
             var files = DirFile.GetFileNames(dirPath);
             var listFiles = new List<string>();
             foreach (var file in files)
             {
                listFiles.Add(file.Replace(dirPath, "icons/32X32/"));
             }
             //缓存一天吧
             CacheHelper.SetCache("SystemIcons", listFiles, new TimeSpan(24, 0, 0));
             return this.JsonFormatSuccess(listFiles, SystemMessage.LoadSuccess.ToString());
         }
         #endregion
         #region 用户信息
         /// <summary>
         /// 获取当前账户登录信息
         /// </summary>
         /// <returns></returns>
        [Description("[Index页面加载用户登录信息]获取当前账户登录信息")]
        [LoginAllowView]
        [LigerUIExceptionResult]
         public ActionResult GetCurrentUser()
         {

             SysCurrentUser CurrentUser = new SysCurrentUser(true);
             var User = new { Title = CurrentUser.Title };
             return this.JsonFormat(User, SysOperate.Load);
         }
        [Description("login登录(带验证码验证),登录成功则跳转")]
        [Anonymous]
        [LigerUIExceptionResult]
        public ActionResult LoginAndRedirect()
        {
            UserRequest request = new UserRequest(HttpContext);
            IUserService userService = new UserService();
            bool status = false;
            if (Session["CheckCode"].ToString ()== request.CheckCode)
            {
                tbUser user = userService.Login(request);
                if (!user.IsNullOrEmpty())
                {
                    status = true;
                    //保存信息到Session
                    SysCurrentUser.SaveUserInfo(user);
                    UserOperateLog.WriteOperateLog("[用户登录]" + SysOperate.Add.ToMessage(user.IsNullOrEmpty()));
                }
                else
                    status = false;
            }
            else
                status = false; ;
            return this.JsonFormat(status, status, SysOperate.Operate);

        }
        [Description("[Index页面Login]登陆")]
        [LoginAllowView]
        [LigerUIExceptionResult]
        public ActionResult Login()
        {
            UserRequest request=new UserRequest (HttpContext);
            IUserService userService=new UserService();
            tbUser user = userService.Login(request);
            //保存信息到Session
            SysCurrentUser.SaveUserInfo(user);
            UserOperateLog.WriteOperateLog("[用户登录]" + SysOperate.Add.ToMessage(user.IsNullOrEmpty()));
            return this.JsonFormat(user,!user.IsNullOrEmpty(), SysOperate.Operate);
        }
        [Description("[Index页面修改密码]修改密码")]
        [LoginAllowView]
        [LigerUIExceptionResult]
        public ActionResult ChangePassword()
        {
            UserRequest request = new UserRequest(HttpContext);
            IUserService userService = new UserService();
            var status = userService.ChangePassword(request,new SysCurrentUser().UserID);
            UserOperateLog.WriteOperateLog("[用户修改密码]" + SysOperate.Operate.ToMessage(status));
            return this.JsonFormat(status, status, SysOperate.Operate);
            
        }
         #endregion
        #region 用户收藏信息
        [Description("[Index页面Load收藏]加载我的收藏信息")]
        [LoginAllowView]
        [LigerUIExceptionResult]
        public ActionResult GetMyFavorite()
        {
            SysCurrentUser user = new SysCurrentUser();
            IFavoriteService service = new FavoriteService();
            IEnumerable<tbFavorite> listFavorite = service.GetEntities(p => p.UserID == user.UserID);
            var data = ViewModelFavorite.ToListViewModel(listFavorite);
            return this.JsonFormat(data, SysOperate.Load);
         

        }
        [Description("[Index页面Add收藏]添加我的收藏信息")]
        [LoginAllowView]
        [LigerUIExceptionResult]
        public ActionResult AddFavorite()
        {
            SysCurrentUser user = new SysCurrentUser();
            //请求参数获取
            FavoriteRequest request = new FavoriteRequest(HttpContext);
            //模块参数和用户参数获取
            IModuleService moduleService = new ModuleService();
            tbModule module = moduleService.GetEntity(p => p.ModuleID ==Convert.ToInt32(request.MenuNo));
            request.GetFavoriteModuleInfo(module);
            IFavoriteService service = new FavoriteService();
            request.Favorite.UserID = user.UserID;
            //添加状态
            bool status = service.Insert(request.Favorite);
           
            UserOperateLog.WriteOperateLog("[收藏信息]" + SysOperate.Add.ToMessage(status));
            return this.JsonFormat(status,status, SysOperate.Add);
        }
        [Description("[Index页面Delete收藏]删除我的收藏信息")]
        [LoginAllowView]
        [LigerUIExceptionResult]
        public ActionResult RemoveFavorite()
        {
            SysCurrentUser user = new SysCurrentUser();
            //请求参数获取
            RequestBase request = new RequestBase(HttpContext);
            IFavoriteService service = new FavoriteService();
          bool status= service.Delete(request.ID);
          return this.JsonFormat(status, status, SysOperate.Delete);
          
        }
        #endregion
    }
}
