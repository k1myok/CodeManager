using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.IDao;
using TZHSWEET.Common;
using System.Data.Objects;
namespace TZHSWEET.EFDao
{
    public class RolePermissionEFDao : BaseEFDao<tbRolePermission>, IRolePermissionDao<tbRolePermission>
    {

        /// <summary>
        /// VRoleGrantPermission分页程序
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="CommandText">Sql语句</param>
        /// <param name="Count">总个数</param>
        /// <returns></returns>
        public IEnumerable<VRoleGrantPermission> GetRolePermission(int pageNumber, int pageSize, string orderName, string sortOrder, string CommandText, out int Count)
        {
            PaginationHelper pager = new PaginationHelper("VRoleGrantPermission", orderName, pageSize, pageNumber, sortOrder, CommandText);
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!CommandText.IsNullOrEmpty())
                    CommandText = "  where " + CommandText;
                Count = Entities.ExecuteStoreQuery<int>("select count(*) as Count from VRoleGrantPermission " + CommandText).SingleOrDefault();
                return Entities.ExecuteStoreQuery<VRoleGrantPermission>(pager.GetSelectTopByMaxOrMinPagination()).ToList();

            }

        }
        /// <summary>
        /// 授予角色权限
        /// </summary>
        ///  <param name="list">权限组</param>
        /// <param name="RoleID">用户角色</param>
        /// <returns></returns>
        public bool GrantRolePermission(List<tbRolePermission> list, int RoleID)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                using (Entities.Connection)
                {
                    Entities.Connection.Open();
                    var tran = Entities.Connection.BeginTransaction();

                    Entities.ExecuteStoreCommand("delete  from  tbRolePermission where RoleID="+RoleID);
                    foreach (tbRolePermission permission in list)
                        Entities.tbRolePermissions.AddObject(permission);
                    if (Entities.SaveChanges() > 0)
                    {
                        tran.Commit();
                        return true;
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
