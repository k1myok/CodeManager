 /*  作者：       tianzh
 *  创建时间：   2012/7/16 15:29:59
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.ViewModel;


namespace TZHSWEET.IBLL
{
    public  interface IBaseService<T>
    {
        /// <summary>
        /// 获得记录总数
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        int GetCount(Func<T, bool> exp);
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetEntities(Func<T, bool> exp);
       /// <summary>
       /// 分页查询
       /// </summary>
       /// <param name="gridData"></param>
       /// <param name="count">返回记录总数</param>
       /// <returns></returns>
        IEnumerable<T> GetAllEntitiesByPaging(LigerUIGridRequest gridData,out int count);
        /// <summary>
        ///查询Entity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        T GetEntity(Func<T, bool> exp);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Insert(T data);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(T data);
        /// <summary>
        /// 删除(存储过程)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool Delete(Object ID);
    }
}
