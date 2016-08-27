using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.IDao;
using System.Data.Objects;
using System.Data;
using TZHSWEET.Common;
namespace TZHSWEET.EFDao
{
    public class UserEFDao : BaseEFDao<tbUser>, IUserDao<tbUser>
    {

       /// <summary>
        /// 获取用户角色拥有的所有菜单
       /// </summary>
       /// <param name="Where">查询条件Entity SQL</param>
       /// <param name="parameters"></param>
       /// <returns></returns>
        public IEnumerable<VRoleMenuPermission> GetUserMenuPermission(string Where, params ObjectParameter[] parameters)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                return Entities.VRoleMenuPermissions.Where(Where, parameters).ToList();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public bool Update(tbUser user, string Roles)
        {
            if (Roles.IsNullOrEmpty())
                return false;
            string[] RoleIDs = Roles.Split(',');
            string RolesData = Roles.Trim(',');//有可能角色出现前缀的,
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                //直接删除该用户的角色
                int Count = Entities.ExecuteStoreCommand("delete from tbUserRole where UserID=" + user.UserID );
                    //再次添加角色
                    foreach (string roleID in RoleIDs)
                    {
                        if (!roleID.IsNullOrEmpty())
                            Entities.tbUserRoles.AddObject(
                                new tbUserRole()
                                    {
                                        UserID=user.UserID,
                                        RoleID = Convert.ToInt32(roleID),
                                        CreateDate = DateTime.Now

                                    });
                    }
                 
                
              
                return Entities.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 添加并关联多个角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public bool Insert(tbUser user, string Roles)
        {
            string[] RoleIDs = Roles.Split(',');
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                var obj = Entities.CreateObjectSet<tbUser>();
               
                user.tbUserRoles = new TrackableCollection<tbUserRole>();

                foreach (string roleID in RoleIDs)
                {
                    if (!roleID.IsNullOrEmpty())
                    user.tbUserRoles.Add(
                        new tbUserRole()
                           {
                               RoleID = Convert.ToInt32(roleID),
                               CreateDate = DateTime.Now

                           });
                }
                obj.AddObject(user);
                return Entities.SaveChanges() > 0;
            }
        }
      
        public override bool Update(tbUser entity)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                var obj = Entities.CreateObjectSet<tbUser>();
                entity.LastLoginTime = DateTime.Now;
                entity.ModifyDate = DateTime.Now;
                obj.Attach(entity);
                Entities.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return Entities.SaveChanges() > 0;
            }
           
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool ChangePassword(tbUser entity,string password)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                var obj = Entities.CreateObjectSet<tbUser>();
                entity.UserPassword = password;
                entity.LastLoginTime = DateTime.Now;
                entity.ModifyDate = DateTime.Now;
                obj.Attach(entity);
                Entities.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return Entities.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 获取用户所有模块权限(增删改查等等)
        /// </summary>
        /// <param name="where">查询条件Entity SQL</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<VRoleModulePermission> GetUserModulePermission(string where, params ObjectParameter[] parameters)
        {

            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                return Entities.VRoleModulePermissions.Where(where, parameters).ToList();
            }
        }
        /// <summary>
        /// 获取用户所有模块权限(sql语句方式)
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<VRoleMenuPermission> GetUserMenuPermissionBySql(string where)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!where.IsNullOrEmpty())
                    where =" where "+ where;
                return Entities.ExecuteStoreQuery<VRoleMenuPermission>("select * from VRoleMenuPermission  " + where).ToList();
            }
        }
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
        public  IEnumerable<VRoleMenuPermission> GetAllMenusForPaging(int pageNumber, int pageSize, string orderName, string sortOrder, string CommandText,out int Number)
        {
            PaginationHelper pager = new PaginationHelper(typeof(VRoleMenuPermission).Name, orderName, pageSize, pageNumber, sortOrder, CommandText);
            pager.GetSelectTopByMaxOrMinPagination();
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!CommandText.IsNullOrEmpty())
                    CommandText = " where " + CommandText;
                //计算count
               Number= Entities.ExecuteStoreCommand("select count(*) from " + typeof(VRoleMenuPermission).Name +CommandText);
                return Entities.ExecuteStoreQuery<VRoleMenuPermission>(pager.GetSelectTopByMaxOrMinPagination()).ToList();
            }

        }
    }
}
