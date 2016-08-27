 /*  作者：       tianzh
 *  创建时间：   2012/7/17 21:09:19
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
 using TZHSWEET.ViewModel;
using Spring.Context;
using Spring.Context.Support;
namespace TZHSWEET.BLL
{
    /// <summary>
    /// 服务层
    /// </summary>
    public class ModulePermissionService : BaseService<tbModulePermission>, IModulePermissionService
    { 
         /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IModulePermissionDao<tbModulePermission> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public ModulePermissionService()
        {
			//spring.net注入
        IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("ModulePermissionDao") as IModulePermissionDao<tbModulePermission>;
            // DaoFactory.GetModulePermissionDao();
            Dao = myDao;
        }
        /// <summary>
        /// 获取菜单的按钮信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public IEnumerable<VModulePermission> GetMenuButtons(LigerUIGridRequest request, out int Count)
        {
            //wher语句
            string commandText = "";
            if (!request.Where.IsNullOrEmpty())
            {
                //做Where的翻译处理工作
                FilterTranslator whereTranslator = new FilterTranslator();
                //当前角色规则转化(用户角色in (xxx)中
                //反序列化Filter Group JSON,并把角色权限组所在的规则合并
                whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(request.Where);
                //开始翻译sql语句
                whereTranslator.Translate();
                commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);
            }
            return myDao.GetModulesPermission(request.PageNumber, request.PageSize, request.SortName, request.SortOrder, commandText, out Count);
        }
        /// <summary>
        /// 获取菜单的按钮信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public LigerUIGrid GetMenuButtons(LigerUIGridRequest request)
        { 
            int count=0;
            var data = ViewModelMenuButtons.ToListViewModel(GetMenuButtons(request, out count));
            return new LigerUIGrid()
            {
                Rows = data,
                Total = count
            };
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="buttons">ViewModel菜单按钮模型</param>
        /// <returns></returns>
        public bool Insert(ViewModelMenuButtons buttons)
        {
          var data= new PermissionService().GetEntity(p=>p.PermissionID==buttons.ButtonID.Value);
               
           if (data.IsNullOrEmpty())
           {
               return false;
           }
           return base.Insert(ViewModelMenuButtons.ToEntity(buttons));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="buttons">ViewModel菜单按钮模型</param>
        /// <returns></returns>
        public bool Update(ViewModelMenuButtons buttons)
        {
            var data = new PermissionService().GetEntity(p => p.PermissionID == buttons.ButtonID.Value);

            if (data.IsNullOrEmpty())
            {
                return false;
            }
            return base.Update(ViewModelMenuButtons.ToEntity(buttons));
        }
       
    }
}
