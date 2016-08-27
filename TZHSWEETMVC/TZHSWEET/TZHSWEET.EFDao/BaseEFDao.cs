 /*  作者：       tianzh
 *  创建时间：   2012/7/16 11:06:16
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.IDao;
using TZHSWEET.Entity;
using System.Data;
using TZHSWEET.Common;
using System.Data.Objects;
namespace TZHSWEET.EFDao
{
    /// <summary>
    /// 公共接口
    /// </summary>
    public  class BaseEFDao<T> : IBaseDao<T> where T : class,new()   //限制T为class
    {
       
        #region 查询普通实现方案(基于Lambda表达式的Where查询)
       /// <summary>
        /// 获取所有Entity
       /// </summary>
       /// <param name="exp">Lambda条件的where</param>
       /// <returns></returns>
        public virtual IEnumerable<T> GetEntities(Func<T, bool> exp)
        {

            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                return Entities.CreateObjectSet<T>().Where(exp).ToList();
            }

        }
        /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(Func<T, bool> exp)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                return Entities.CreateObjectSet<T>().Where(exp).Count();
            }
        }
        /// <summary>
        /// 分页查询(Linq分页方式)
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetEntitiesForPaging(int pageNumber, int pageSize, Func<T, string> orderName, string sortOrder, Func<T, bool> exp)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (sortOrder=="asc") //升序排列
                {
                    return Entities.CreateObjectSet<T>().Where(exp).OrderBy(orderName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                    return Entities.CreateObjectSet<T>().Where(exp).OrderByDescending(orderName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

        }
        /// <summary>
        /// 根据条件查找
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual T GetEntity(Func<T, bool> exp)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                return Entities.CreateObjectSet<T>().Where(exp).SingleOrDefault();
            }
        }
        #endregion
        #region 查询Entity To Sql语句外接接口的查询实现
        /// <summary>
        /// 获取所有Entity(立即执行请使用ToList()
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="objParams">可变参数</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetEntities(string commandText)
        {

            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!commandText.IsNullOrEmpty())
                    commandText = " where " + commandText;
                return Entities.ExecuteStoreQuery<T>("select * from "+typeof(T).Name+commandText).ToList();
            }

        }
        /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="commandText">>Sql where语句</param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(string tableName, string commandText)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!commandText.IsNullOrEmpty())
                    commandText = " where " + commandText;
                return Entities.ExecuteStoreQuery<T>("select * from " + tableName + commandText).Count();
            }
        }
       /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="CommandText">Sql语句</param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(string CommandText)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                if (!CommandText.IsNullOrEmpty())
                    CommandText = " where " + CommandText;
                return Entities.ExecuteStoreQuery<T>("select * from " +typeof(T).Name+ CommandText).Count();
            }
        }

        /// <summary>
        /// 分页查询(Linq分页方式)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="CommandText">Sql语句</param>
        /// <param name="Count">总个数</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetEntitiesForPaging(string tableName, int pageNumber, int pageSize, string orderName, string sortOrder, string CommandText,out int Count)
        {
            PaginationHelper pager = new PaginationHelper(tableName, orderName, pageSize, pageNumber, sortOrder, CommandText);
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
               Count =GetEntitiesCount(tableName,CommandText);
               return Entities.ExecuteStoreQuery<T>(pager.GetSelectTopByMaxOrMinPagination()).ToList();

            }

        }
       /// <summary>
        /// 根据条件查找
       /// </summary>
        /// <param name="CommandText">Sql语句</param>
        /// <param name="objParams">可变参数</param>
       /// <returns></returns>
        public virtual T GetEntity(string CommandText)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                return Entities.ExecuteStoreQuery<T>("select * from "+typeof(T).Name+" where "+CommandText).SingleOrDefault();
            }
        }
        #endregion
        #region 增删改实现
        /// <summary>
        /// 插入Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Insert(T entity)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                var obj = Entities.CreateObjectSet<T>();
                obj.AddObject(entity);
                return Entities.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 更新Entity(注意这里使用的傻瓜式更新,可能性能略低)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Update(T entity)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                var obj = Entities.CreateObjectSet<T>();
                obj.Attach(entity);
                Entities.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return Entities.SaveChanges() > 0;
            }

        }
        /// <summary>
        /// 删除Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                var obj = Entities.CreateObjectSet<T>();

                if (entity != null)
                {
                    obj.Attach(entity);
                    Entities.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
                    obj.DeleteObject(entity);
                    return Entities.SaveChanges() > 0;

                }
                return false;
            }
        }
        /// <summary>
        /// 删除实现 存储过程实现方式(调用spDelete+表名+ 主键ID)
        /// </summary>
        /// <param name="ID">删除的主键</param>
        /// <returns></returns>
        public virtual bool Delete(object ID)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                //存储过程实现方式(调用spDelete+表名+ 主键ID),存储过程命名为spDelete+表名的格式
              return  Entities.ExecuteStoreCommand("spDelete" + typeof(T).Name + " " + ID.ToString ())>0;
                
            }
        }

     
        #endregion
      

   
    }
}
