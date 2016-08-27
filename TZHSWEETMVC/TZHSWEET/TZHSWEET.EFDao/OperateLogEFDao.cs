 /*  作者：       tianzh
 *  创建时间：   2012/7/21 23:50:54
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.IDao;
using System.Data.Objects;
using System.Data;

namespace TZHSWEET.EFDao
{
    public class OperateLogEFDao : BaseEFDao<tbOperateLog>, IOperateLogDao<tbOperateLog>
    {
        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Delete(tbOperateLog entity)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                   ObjectParameter[] objParams = new ObjectParameter[]{ 
                new ObjectParameter("ID",entity.ID)
            };
                //检验是否有删除权限(只能删除一个月以前的日志)
                   bool DeleteStatus = Entities.CreateObjectSet<tbOperateLog>().Where(" DATEADD(MONTH,1,it.CreateDate)<getdate() and it.ID=@ID", objParams).Count() > 0;
                //删除处理   
                if (DeleteStatus)
                   {
                       var obj = Entities.CreateObjectSet<tbOperateLog>();
                       obj.Attach(entity);
                       Entities.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
                       return Entities.SaveChanges() > 0;
                   }
                   else

                       return false;
            }
          
        }
       
        /// <summary>
        /// 按照月份删除
        /// </summary>
        /// <param name="month">删除多少月份以前的数据</param>
        /// <returns></returns>
        public bool DeleteByMonth(int month)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {

            //    ObjectParameter[] objParams = new ObjectParameter[]{ 
            //    new ObjectParameter("month",month.ToString ())
            //};
                //直接使用sql删除
                return Entities.ExecuteStoreCommand("delete  from tbOperateLog where DATEADD(MONTH,"+month+",CreateDate)<getdate()") > 0;
            }
        }
    }
}
