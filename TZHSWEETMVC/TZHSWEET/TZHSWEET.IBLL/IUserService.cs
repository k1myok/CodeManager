
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.Common;
using TZHSWEET.ViewModel;

namespace TZHSWEET.IBLL
{
    public interface IUserService : IBaseService<tbUser>
    {

        #region 业务逻辑
        #region 用户信息
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="Number">帐号</param>
        /// <param name="Password">密码</param>
        /// <returns>返回用户实体</returns>
        tbUser Login(string Number, string Password);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        tbUser Login(UserRequest requestInfo);
         /// <summary>
        /// 根据条件获取所有用户
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <param name="Count">记录总数</param>
        /// <returns></returns>
        IEnumerable<tbUser> GetAllUsers(LigerUIGridRequest request, out int Count);
         /// <summary>
        /// 根据条件获取所有菜单
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <returns></returns>
        LigerUIGrid GetAllUsers(LigerUIGridRequest request);
        /// <summary>
        /// 获取用户所有的角色信息
        /// </summary>
        /// <param name="User">用户信息实体</param>
        /// <returns>用户所有的角色用,隔开</returns>
        string GetUserAllRole(tbUser User);
         /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
         bool Update(tbUser user, string Roles);
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
        bool Insert(tbUser user, string Roles);
        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        string GetUserAllRole(int UserID);
        /// <summary>
        /// 获取用户实体信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        tbUser GetUserInfo(string UserID);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        bool ChangePassword(UserRequest requestInfo, int UserID);

        #endregion

        #endregion

        #region 菜单
        /// <summary>
        /// 获取指定角色(多个)的菜单
        /// </summary>
        /// <param name="Roles">角色组(多个角色用,分隔开)</param>
        /// <returns></returns>
        IEnumerable<VRoleMenuPermission> GetRoleMenu(string Roles);
        /// <summary>
        /// 获取用户的菜单
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        IEnumerable<ViewModelMenu> GetUserPermissionMenus(string Roles);

        /// <summary>
        /// 请求一级菜单树
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<ViewModelMenu> GetUserTreeMenus(LigerUITreeRequest request, string userRoles);
        /// <summary>
        /// 获取请求的Grid菜单列表
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="userRoles">角色组</param>
        /// <param name="count">总记录数</param>
        /// <returns></returns>
        IEnumerable<VRoleMenuPermission> GetUserGridMenus(LigerUIGridRequest request, string userRoles,out int count);
        /// <summary>
        /// 根据whereFilter条件获取子级数据
        /// </summary>
        /// <param name="request">where条件</param>
        /// <param name="userRoles">角色组</param>
        /// <returns></returns>
        IEnumerable<ViewModelMenu> GetMenusByParentNo(string whereFilter, string userRoles);
        /// <summary>
        /// 获取角色指定菜单的按钮信息
        /// </summary>
        /// <param name="Roles">角色信息</param>
        /// <param name="RequestController">请求信息</param>
        /// <param name="AdminUserRoleID">超级管理员角色ID</param>
        /// <returns></returns>
        IEnumerable<ViewModelMyButton> GetButtons(string Roles, ButtonRequest RequestController, string AdminUserRoleID);
          /// <summary>
        /// 获取请求的Grid菜单列表的LigeruiGrid格式
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="userRoles">角色组</param>
        /// <param name="AdminUserRoleID">超级管理员角色ID</param>
        /// <returns></returns>
        LigerUIGrid GetUserGridMenus(LigerUIGridRequest request, string userRoles, string AdminUserRoleID);
       
        #endregion
        #region 权限验证
        /// <summary>
        /// 角色是否拥有权限
        /// </summary>
        /// <param name="Roles">角色组</param>
        /// <param name="Controller">控制器</param>
        /// <param name="Action">动作</param>
        /// <returns></returns>
        bool RoleHasOperatePermission(string Roles, string Controller, string Action);
        /// <summary>
        /// 角色是否拥有该菜单模块权限
        /// </summary>
        /// <param name="Roles">角色组</param>
        /// <param name="Controller">控制器</param>
        /// <param name="Action">动作</param>
        /// <returns></returns>
        bool RoleHasMenusPermission(string Roles, string Controller, string Action);

        #endregion
    }
}
