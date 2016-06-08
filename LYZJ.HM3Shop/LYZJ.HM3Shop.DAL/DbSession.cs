using LYZJ.HM3Shop.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.DAL
{
    /// <summary>
    /// 数据库交互会话，
    /// 如果操作数据库的话直接从这里来操作
    /// </summary>
    public partial class DbSession:IDbSession //代表的是应用程序跟数据库之间的一次会话，也是数据库访问层的统一入口
    {
        #region ---由T4模版自动生成---
        //拿到数据访问层接口的仓储，数据访问层的统一入口
        //public IDAL.IRoleRepository RoleRepository
        //{ 
        //    get
        //    { 
        //        return new RoleRepository();
        //    } 
        //}

        //public IDAL.IUserInfoRepository UserInfoRepository
        //{
        //    get
        //    {
        //        return new UserInfoRepository();
        //    }
        //} 
        #endregion

        /// <summary>
        /// 代表当前应用程序跟数据库的回话内所有的实体变化，更新会数据库
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()  //UintWork单元工作模式
        {   
            //调用EF上下文的SaveChanges的方法
            return DAL.EFContextFactory.GetCurrentDbContext().SaveChanges();

        }

        //如何执行SQL语句呢？？？？
        //public int ExcuteSql(string strSql, DbParameter[] parameters)
        //{
              //封装一个执行的SQL脚本
        //    return DAL.EFContextFactory.GetCurrentDbContext().ExecuteFunction(strSql, parameters);
        //}

        public int ExcuteSql(string strSql, DbParameter[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}
