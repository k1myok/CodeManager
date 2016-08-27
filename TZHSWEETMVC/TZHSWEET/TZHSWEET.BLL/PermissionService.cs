 /*  作者：       tianzh
 *  创建时间：   2012/7/17 21:09:26
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
    public class PermissionService : BaseService<tbPermission>, IPermissionService
    { 
         /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IPermissionDao<tbPermission> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public PermissionService()
        {
			//spring.net注入
        IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("PermissionDao") as IPermissionDao<tbPermission>;
            // DaoFactory.GetPermissionDao();
            Dao = myDao;
        }
        /// <summary>
        /// 获取所有的权限列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<ViewModelButton> GetPermission(LigerUIGridRequest request)
        {
            return ViewModelButton.ToListViewModel(myDao.GetEntitiesForPaging(request.PageNumber, request.PageSize, p => request.SortName, request.SortOrder, p => true));

        }
        /// <summary>
        /// 根据条件获取权限信息
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="Count">总个数</param>
        /// <returns></returns>
        public IEnumerable<tbPermission> GetPermission(LigerUIGridRequest request, out int Count)
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
                //把ViewModel参数替换成数据库实体
                commandText = ViewModelButton.FilterParamsToEntityParams(commandText);
            }
                return myDao.GetEntitiesForPaging("tbPermission", request.PageNumber, request.PageSize, request.SortName, request.SortOrder, commandText, out Count);
        }
        /// <summary>
        /// 获得权限树(此方法一次性读出所有数据,如果数据量过大请考虑用GridTree格式请求）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public LigerUIGrid GetPermissionGridTree(LigerUIGridRequest request)
        {
            int count = 0;
            
            var data = GetEntities(p => true);
            count = GetCount(p => true);
            //GetPermission(request, out count);
            List<ViewModelButton> listPermission = new List<ViewModelButton>();
            //查找所有的一级权限
            var ParentPermission = data.Where(con => con.ParentID.Value == 0);
            foreach (var parent in ParentPermission)
            {
                //实体转化 
                ViewModelButton parentItem = ViewModelButton.ToViewModel(parent);
                //获取子级
                GetPermissionChildren(ref parentItem, data.ToList());
                listPermission.Add(parentItem);
            }
            return new LigerUIGrid()
            {
                Rows = listPermission,
                Total = count
            };

        }
        /// <summary>
        /// 获取子集
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="allList"></param>
        public void GetPermissionChildren(ref ViewModelButton parent, List<tbPermission> allList)
        {
            foreach (tbPermission permission in allList)
            {

                if (permission.ParentID == parent.ID)
                {

                    //实体转化
                    ViewModelButton child = ViewModelButton.ToViewModel(permission);
                    if (parent.children == null)
                        parent.children = new List<ViewModelButton>();
                    //添加到父级的Children中
                    parent.children.Add(child);
                    GetPermissionChildren(ref child, allList);//递归添加子树
                }
            }
        }
        /// <summary>
        /// 请求权限列表信息
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        public LigerUIGrid GetPermissionForGrid(LigerUIGridRequest request)
        {
            int count = 0;
            return new LigerUIGrid()
            {
                Rows =ViewModelButton.ToListViewModel(GetPermission(request,out count)),
                Total = count//myDao.GetEntitiesCount(p => true)
            };
        }
        /// <summary>
        /// 判断是否存在该权限(根据控制器和动作)
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="action">动作</param>
        /// <returns></returns>
        public bool IsPermissionExist(string controller,string action)
        {
            //检查是否在tbPermission中存在该权限,如果存在,则不显示
            var data = GetEntity(p => p.PermissionAction == controller && p.PermissionAction == action);
            //判断是否拥有权限
            if (!data.IsNullOrEmpty())
            {
                return true;
            }
            return false;
        }
    }
}
