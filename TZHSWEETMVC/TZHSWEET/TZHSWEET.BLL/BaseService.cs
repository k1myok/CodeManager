 /*  作者：       tianzh
 *  创建时间：   2012/7/16 23:56:40
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.IBLL;
using TZHSWEET.IDao;
using TZHSWEET.ViewModel;
using TZHSWEET.Common;

namespace TZHSWEET.BLL
{
    public class BaseService<T> : IBaseService<T> where T :class,new()
    {
        /// <summary>
        /// EF实例化(注意:基类只负责增删改查)
        /// </summary>
        private IBaseDao<T> dao=null;
        public IBaseDao<T> Dao   //IOC依赖注入
        {
            get { return dao; }
            set { this.dao =(IBaseDao<T>) value; }
        }

        #region 增删改查
        public virtual IEnumerable<T> GetEntities(Func<T, bool> exp)
        {
            return dao.GetEntities(exp);
         
        }
        public virtual int GetCount(Func<T, bool> exp)
        {
            return dao.GetEntitiesCount(exp);
        }
       /// <summary>
        /// 分页查询
       /// </summary>
       /// <param name="gridData">请求</param>
       /// <param name="Count">总记录数</param>
       /// <returns></returns>
        public virtual IEnumerable<T> GetAllEntitiesByPaging(LigerUIGridRequest gridData,out int Count)
        {
             //做Where的翻译处理工作
                FilterTranslator whereTranslator = new FilterTranslator();
            if (!gridData.Where.IsNullOrEmpty())//没有where条件
            {
                //注入当前用户的ID和UserRole角色配置信息
                FilterTranslator.RegCurrentParmMatch("{CurrentUserID}", () => SessionHelper.Get("UserID").ObjToInt());
                FilterTranslator.RegCurrentParmMatch("{CurrentRoleID}", () => SessionHelper.Get("UserRoles")[0].ObjToInt());
                //反序列化Filter Group JSON
                whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(gridData.Where);
                //开始翻译sql语句
                whereTranslator.Translate();
                string commandText=FilterParam.AddParameters(whereTranslator.CommandText,whereTranslator.Parms);

                return dao.GetEntitiesForPaging(typeof(T).Name, gridData.PageNumber, gridData.PageSize, gridData.SortName, gridData.SortOrder, commandText,out Count);
            }
            Count = dao.GetEntitiesCount(q => true);
            return dao.GetEntitiesForPaging(gridData.PageNumber, gridData.PageSize,p=> gridData.SortName, gridData.SortOrder, q => true);
        }

        public virtual T GetEntity(Func<T, bool> exp)
        {
            return dao.GetEntity(exp);
        }

        public virtual bool Insert(T data)
        {
            return dao.Insert(data);
        }

        public virtual bool Update(T data)
        {
            return dao.Update(data);
        }

        public virtual bool Delete(T data)
        {
            return dao.Delete(data);
        }
        public virtual bool Delete(Object ID)
        {
            return dao.Delete(ID);
        }
        #endregion



       
    }
}
