
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using System.Data.Objects;

namespace TZHSWEET.IDao
{
    public interface IUserDao<T> : IBaseDao<T> where T : class
    {

        /// <summary>
        /// 获取用户角色拥有的所有菜单
       /// </summary>
       /// <param name="Where">查询条件Entity SQL</param>
       /// <param name="parameters"></param>
       /// <returns></returns>
         IEnumerable<VRoleMenuPermission> GetUserMenuPermission(string Where, params ObjectParameter[] parameters);
        /// <summary>
        /// 获取用户所有模块权限(增删改查等等)
        /// </summary>
        /// <param name="Where">查询条件Entity SQL</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
         IEnumerable<VRoleModulePermission> GetUserModulePermission(string Where, params ObjectParameter[] parameters);
          /// <summary>
        /// 获取用户所有模块权限(sql语句方式)
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
         IEnumerable<VRoleMenuPermission> GetUserMenuPermissionBySql(string where);
         /// <summary>
        /// 分页查询(获取菜单分页)
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="CommandText">Sql语句</param>
        /// <param name="Number">总记录数</param>
        /// <returns></returns>
         IEnumerable<VRoleMenuPermission> GetAllMenusForPaging(int pageNumber, int pageSize, string orderName, string sortOrder, string CommandText,out int Number);
         /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
          bool ChangePassword(tbUser entity, string password);
          /// <summary>
        /// 添加并关联多个角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
          bool Insert(tbUser user, string Roles);
         /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
          bool Update(tbUser user, string Roles);
    }
}
