 /*  作者：       tianzh
 *  创建时间：   2012/7/17 21:09:35
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.IBLL;
using TZHSWEET.Entity;
using TZHSWEET.IDao;
using TZHSWEET.Common;

using Spring.Context;
using Spring.Context.Support;
namespace TZHSWEET.BLL
{
    /// <summary>
    /// 服务层
    /// </summary>
    public class UserRoleService : BaseService<tbUserRole>, IUserRoleService
    { 
         /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IUserRoleDao<tbUserRole> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public UserRoleService()
        {
			//spring.net注入
        IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("UserRoleDao") as IUserRoleDao<tbUserRole>;
            // DaoFactory.GetUserRoleDao();
            Dao = myDao;
        }
       
      

    }
}
