
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;

namespace TZHSWEET.IDao
{
    public interface IModulePermissionDao<T> : IBaseDao<T> where T : class
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
         IEnumerable<VModulePermission> GetModulesPermission(int pageNumber, int pageSize, string orderName, string sortOrder, string CommandText, out int Count);
    }
}
