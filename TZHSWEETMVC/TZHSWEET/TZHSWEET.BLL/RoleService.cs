 /*  作者：       tianzh
 *  创建时间：   2012/7/17 21:09:32
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
    public class RoleService : BaseService<tbRole>, IRoleService
    {
        #region 依赖注入
        /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IRoleDao<tbRole> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public RoleService()
        {
            //spring.net注入
            IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("RoleDao") as IRoleDao<tbRole>;
            // DaoFactory.GetRoleDao();
            Dao = myDao;
        } 
        #endregion

        /// <summary>
        /// 根据条件获取所有角色
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <param name="Count">记录总数</param>
        /// <returns></returns>
        public IEnumerable<tbRole> GetAllRoles(LigerUIGridRequest request, out int Count)
        {  
            //wher语句
            string commandText = "";
            if (!request.Where.IsNullOrEmpty())
            {
                //做Where的翻译处理工作
                FilterTranslator whereTranslator = new FilterTranslator();
                //反序列化Filter Group JSON
                whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(request.Where);
                //开始翻译sql语句
                whereTranslator.Translate();
                commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);
            }
                return myDao.GetEntitiesForPaging("tbRole", request.PageNumber, request.PageSize, request.SortName, request.SortOrder, commandText, out Count);
        }
        /// <summary>
        /// 根据条件获取所有角色
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <returns></returns>
        public LigerUIGrid GetAllRoles(LigerUIGridRequest request)
        {
            int total = 0;
            return new LigerUIGrid()
            {
                Rows =ViewModelRole.ToListViewModel(GetAllRoles(request, out total)),
                Total = total
            };
        }
        #region 请求角色树形
        /// <summary>
        /// 请求角色树形
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<LigerUITree> GetRoleTree(LigerUITreeRequest request)
        {
            //wher语句查询条件
            string commandText = "";
            if (!request.Where.IsNullOrEmpty())
            {
                //做Where的翻译处理工作
                FilterTranslator whereTranslator = new FilterTranslator();
                //反序列化Filter Group JSON
                whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(request.Where);
                //开始翻译sql语句
                whereTranslator.Translate();
                commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);
            }

            //返回ui层的菜单
            IEnumerable<LigerUITree> rootRole = new List<LigerUITree>(){
            new LigerUITree()
            {

                icon = request.RootIcon,
                id=0,
                 desc="角色组",
                text = "角色组",
                children = (List<LigerUITree>)LigerUITree.ToListViewModel(myDao.GetEntities(commandText)), 
            
            }
        };
            return rootRole;

        }
        #endregion
        /// <summary>
        /// 获取角色的Select数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<LigerUISelect> GetRolesForSelect(LigerUISelectRequest request)
        {
            //wher语句
            string commandText = "";
            if (!request.Where.IsNullOrEmpty())
            {
                //做Where的翻译处理工作
                FilterTranslator whereTranslator = new FilterTranslator();
                //反序列化Filter Group JSON
                whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(request.Where);
                //开始翻译sql语句
                whereTranslator.Translate();
                commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);
            }
            return  LigerUISelect.ToListModel(myDao.GetEntities(commandText));
        }
      
    }
}
