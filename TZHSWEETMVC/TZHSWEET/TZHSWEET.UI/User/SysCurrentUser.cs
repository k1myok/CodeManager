 /*  作者：       tianzh
 *  创建时间：   2012/7/20 15:50:48
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Common;
using TZHSWEET.IBLL;
using TZHSWEET.BLL;
using TZHSWEET.Entity;

namespace TZHSWEET.UI
{
    /// <summary>
    /// 获取当前User状态
    /// </summary>
   public class SysCurrentUser
    {
       /// <summary>
       /// 用户昵称
       /// </summary>
       public string Title { get; set; }
       /// <summary>
       /// 用户ID
       /// </summary>
       public int UserID { get; set; }
       /// <summary>
       /// 用户角色
       /// </summary>
       public string UserRoles { get; set; }
       /// <summary>
       /// 用户信息
       /// </summary>
       public tbUser User { get; set; }
       public DateTime? CurrentUserLastLoginTime { get; set; }
       /// <summary>
       /// 实例化
       /// </summary>
       public SysCurrentUser()
       {
           //if (SessionHelper.Get("UserID") == null)
           //{
           //    throw new ArgumentNullException("用户未登录");
           //}
           //获取当前信息
           UserID = GetCurrentUserID();
           UserRoles = GetCrrentUserRoles();
           
       }
       /// <summary>
       ///保存用户信息到Session中
       /// </summary>
       /// <param name="user"></param>
       public static void SaveUserInfo(tbUser user)
       {
           //如果用户存在
           if (!user.IsNullOrEmpty())
           {
               SessionHelper.SetSession("UserID", user.UserID);
               //读取角色信息
               IUserService service = new UserService();
              string UserRoles = service.GetUserAllRole(user.UserID);
               SessionHelper.SetSession("UserRoles", UserRoles);
               //设置前台显示的昵称信息
               SessionHelper.SetSession("Title", user.NickName);
               SessionHelper.SetSession("CurrentUserLastLoginTime", user.LastLoginTime);
            
           }
       }
       /// <summary>
       /// 是否加载用户详细信息
       /// </summary>
       /// <param name="IsLoadUserInfo">是否需要加载用户信息</param>
       public SysCurrentUser(bool IsLoadUserInfo)
       {
           if (SessionHelper.Get("UserID").IsNullOrEmpty())
           {
               throw new ArgumentNullException("用户未登录");
           }
           //获取当前信息
           UserID = GetCurrentUserID();
           UserRoles = GetCrrentUserRoles();
           //加载用户详细信息
           if (IsLoadUserInfo)
           {
               User = GetCurrentUserInfo();
               Title = GetCurrentTitle();
               SessionHelper.SetSession("CurrentUserLastLoginTime", User.LastLoginTime.ToString ());
           }
       }
       /// <summary>
       /// 获取当前用户角色
       /// </summary>
       /// <returns></returns>
       public string GetCrrentUserRoles()
       {
           
           if (SessionHelper.Get("UserRoles") == null)
           {
               IUserService service = new UserService();
               UserRoles = service.GetUserAllRole(UserID);
               SessionHelper.SetSession("UserRoles", UserRoles);
           }
           else
               UserRoles = SessionHelper.Get("UserRoles");
           return UserRoles;
       }
       /// <summary>
       /// 获取当前用户UserID信息
       /// </summary>
       /// <returns></returns>
       private int GetCurrentUserID()
       {
           int userID=0;
           if (SessionHelper.Get("UserID") != null)
               userID = Convert.ToInt32(SessionHelper.Get("UserID"));
           return userID;
       }
       /// <summary>
       /// 获取当前用户信息
       /// </summary>
       /// <returns></returns>
       public tbUser GetCurrentUserInfo()
       {
           IUserService userService = new UserService();
           return userService.GetEntity(p => p.UserID == UserID);
       }
       /// <summary>
       /// 获取当前用户的昵称信息
       /// </summary>
       /// <returns></returns>
       public string GetCurrentTitle()
       {
           string title = "";
         
               if (SessionHelper.Get("Title") == null)
               {
                   title = User.NickName;
                   SessionHelper.SetSession("Title", title);
               }
               else
               {
                   title = (string)SessionHelper.Get("Title");
               }
               return title;
       }
       /// <summary>
       /// 上次访问时间
       /// </summary>
       /// <returns></returns>
       public string GetCurrentUserLastLoginTime()
       {
           if (SessionHelper.Get("CurrentUserLastLoginTime") == null)
           {
               CurrentUserLastLoginTime = User.LastLoginTime;
              SessionHelper.SetSession("CurrentUserLastLoginTime", CurrentUserLastLoginTime);
              return CurrentUserLastLoginTime.Value.ToString();
           }
           else
           {
               return SessionHelper.Get("CurrentUserLastLoginTime");
           }

       }
    }
}
