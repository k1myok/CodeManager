using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.IDao;
using TZHSWEET.Common;
namespace TZHSWEET.EFDao
{
    public class ModulePermissionEFDao : BaseEFDao<tbModulePermission>, IModulePermissionDao<tbModulePermission>
    {
        /// <summary>
        /// VModulePermission分页程序
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="CommandText">Sql语句</param>
        /// <param name="Count">总个数</param>
        /// <returns></returns>
        public IEnumerable<VModulePermission> GetModulesPermission(int pageNumber, int pageSize, string orderName, string sortOrder, string CommandText,out int Count)
        {
            PaginationHelper pager = new PaginationHelper("VModulePermission", orderName, pageSize, pageNumber, sortOrder, CommandText);
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!CommandText.IsNullOrEmpty())
                    CommandText = "  where " + CommandText;
                Count= Entities.ExecuteStoreQuery<int>("select count(*) as Count from VModulePermission " + CommandText).SingleOrDefault();
               return Entities.ExecuteStoreQuery<VModulePermission>(pager.GetSelectTopByMaxOrMinPagination()).ToList();

            }

        }
        /// <summary>
        /// 计算总数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int GetCount(string tableName,string where)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!where.IsNullOrEmpty())
                    where = "  where " + where;
                return  Entities.ExecuteStoreQuery<int>("select count(*) from " + tableName + where).ObjToInt();
            }
        }
    }
}
