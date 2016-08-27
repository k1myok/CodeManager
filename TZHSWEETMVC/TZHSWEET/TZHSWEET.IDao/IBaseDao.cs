 /*  作者：       tianzh
 *  创建时间：   2012/7/16 11:01:34
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace TZHSWEET.IDao
{
   public interface IBaseDao<T>// where T:class //限制class
    {
        #region 查询普通实现方案(基于Lambda表达式的Where查询)
        /// <summary>
        /// 获取所有Entity
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        IEnumerable<T> GetEntities(Func<T, bool> exp);
        
        /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        int GetEntitiesCount(Func<T, bool> exp);
      
        /// <summary>
        /// 分页查询(Linq分页方式)
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        IEnumerable<T> GetEntitiesForPaging(int pageNumber, int pageSize, Func<T, string> orderName, string sortOrder, Func<T, bool> exp);
       
        /// <summary>
        /// 根据条件查找
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        T GetEntity(Func<T, bool> exp);
       
        #endregion
        #region 查询Sql语句外接接口的查询实现
        /// <summary>
        /// 获取所有Entity(立即执行请使用ToList()
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="objParams">可变参数</param>
        /// <returns></returns>
        IEnumerable<T> GetEntities(string commandText);
          /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="commandText">>Sql where语句</param>
        /// <returns></returns>
         int GetEntitiesCount(string tableName, string commandText);
        /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="objParams">可变参数</param>
        /// <returns></returns>
        int GetEntitiesCount(string commandText);
      
       /// <summary>
        /// 分页查询(Linq分页方式)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="commandText">Sql语句</param>
        /// <param name="count">总个数</param>
        /// <returns></returns>
        IEnumerable<T> GetEntitiesForPaging(string tableName, int pageNumber, int pageSize, string orderName, string sortOrder, string commandText, out int count);
      
        /// <summary>
        /// 根据条件查找
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="objParams">可变参数</param>
        /// <returns></returns>
        T GetEntity(string commandText);

        #endregion
       /// <summary>
       /// 插入Entity
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       bool Insert(T entity);
       /// <summary>
       /// 更新Entity
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       bool Update(T entity);
       /// <summary>
       /// 删除Entity
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       bool Delete(T entity);
         /// <summary>
        /// 删除实现 存储过程实现方式(调用spDelete+表名+ 主键ID)
        /// </summary>
        /// <param name="ID">删除的主键</param>
        /// <returns></returns>
       bool Delete(object ID);
    }
}
