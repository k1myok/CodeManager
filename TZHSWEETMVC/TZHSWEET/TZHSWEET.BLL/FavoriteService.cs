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
    public class FavoriteService : BaseService<tbFavorite>, IFavoriteService
    { 
         /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IFavoriteDao<tbFavorite> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public FavoriteService()
        {
			//spring.net注入
        IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("FavoriteDao") as IFavoriteDao<tbFavorite>;
            // DaoFactory.GetFavoriteDao();
            Dao = myDao;
        }
      

    }
}
