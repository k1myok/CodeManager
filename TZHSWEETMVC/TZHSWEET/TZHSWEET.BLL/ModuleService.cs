 /*  作者：       tianzh
 *  创建时间：   2012/7/17 21:09:22
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
using TZHSWEET.ViewModel;
namespace TZHSWEET.BLL
{
    /// <summary>
    /// 服务层
    /// </summary>
    public class ModuleService : BaseService<tbModule>, IModuleService
    { 
         /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IModuleDao<tbModule> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public ModuleService()
        {
			//spring.net注入
        IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("ModuleDao") as IModuleDao<tbModule>;
            // DaoFactory.GetModuleDao();
            Dao = myDao;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool Insert(TZHSWEET.Entity.tbModule data)
        {
            return base.Insert(data);
        }
        /// <summary>
        /// 根据条件获取所有菜单
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <param name="Count">记录总数</param>
        /// <returns></returns>
        public IEnumerable<tbModule> GetAdminMenus(LigerUIGridRequest request, out int Count)
        {
            //wher语句
            string commandText = "";
            //做Where的翻译处理工作
            FilterTranslator whereTranslator = new FilterTranslator();
            //注入当前用户的ID和UserRole角色配置信息
            FilterTranslator.RegCurrentParmMatch("{CurrentUserID}", () => SessionHelper.Get("UserID").ObjToInt());
            FilterTranslator.RegCurrentParmMatch("{CurrentRoleID}", () => SessionHelper.Get("UserRoles")[0].ObjToInt());
            //当前角色规则转化(用户角色in (xxx)中
            string where = ViewModelMenu.FilterParamsToEntityParams(request.Where);
            //反序列化Filter Group JSON
            whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(where);
            //开始翻译sql语句
            whereTranslator.Translate();
            commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);
            return myDao.GetEntitiesForPaging("tbModule", request.PageNumber, request.PageSize, request.SortName, request.SortOrder, commandText, out Count);
        }
      

    }
}
