/*  作者：       tianzh
*  创建时间：   2012/7/17 21:09:29
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
    public class RolePermissionService : BaseService<tbRolePermission>, IRolePermissionService
    {
        #region - 变量 -

        /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IRolePermissionDao<tbRolePermission> myDao = null;

        #endregion

        #region - 构造函数 -

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public RolePermissionService()
        {
            //spring.net注入
            IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("RolePermissionDao") as IRolePermissionDao<tbRolePermission>;
            // DaoFactory.GetRolePermissionDao();
            Dao = myDao;
        }

        #endregion


        #region 获取角色权限列表信息
        /// <summary>
        /// 获取角色权限列表信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<VRoleGrantPermission> GetPermissionForGreeTree(LigerUIGridRequest request, out int count)
        {
            //wher语句(请保证前台的条件中数据(ViewModel)与数据库字段一致)
            string commandText = FilterHelper.GetFilterTanslate(request.Where);
            return myDao.GetRolePermission(request.PageNumber, request.PageSize, request.SortName, request.SortOrder, commandText, out count);
        }
        #endregion
        #region  获取角色权限Grid信息(如果数据量大需要考虑用异步的GridTree)
        /// <summary>
        /// 获取角色权限Grid信息(如果数据量大需要考虑用异步的GridTree)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public LigerUIGrid GetPermissionForGridTree(LigerUIGridRequest request)
        {
            int count = 0;
            var data = GetPermissionForGreeTree(request, out count);
            List<ViewModelRolePermission> listDepart = new List<ViewModelRolePermission>();
            //查找所有的一级权限
            var ParentPermission = data.Where(con => con.ParentID.Value == 0);
            foreach (var parent in ParentPermission)
            {
                //实体转化 
                ViewModelRolePermission parentItem = ViewModelRolePermission.ToViewModel(parent);
                //获取子级
                GetRolePermissionChildren(ref parentItem, data.ToList());
                listDepart.Add(parentItem);
            }
            return new LigerUIGrid()
            {
                Rows = listDepart,
                Total = count
            };
        }
        #endregion
        #region 获取所有角色权限的json格式
        /// <summary>
        /// 获取所有角色权限的json格式
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public object GetAllRolePermission(LigerUIGridRequest grid)
        {

            string commandText = FilterHelper.GetFilterTanslate(grid.Where);
            var data = myDao.GetEntities(commandText).Select(p => new { RoleID = p.RoleID, RolePermissionID = p.RolePermissionID, ModulePermissionID = p.ModulePermissionID });
            return data;

        }
        #endregion
        #region 获取子集
        /// <summary>
        /// 获取子集
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="allList"></param>
        public void GetRolePermissionChildren(ref ViewModelRolePermission parent, List<VRoleGrantPermission> allList)
        {
            foreach (VRoleGrantPermission permission in allList)
            {

                if (permission.ParentID == parent.PermissionID)
                {

                    //实体转化
                    ViewModelRolePermission child = ViewModelRolePermission.ToViewModel(permission);
                    if (parent.children == null)
                        parent.children = new List<ViewModelRolePermission>();
                    //添加到父级的Children中
                    parent.children.Add(child);
                    GetRolePermissionChildren(ref child, allList);//递归添加子树
                }
            }
        }
        #endregion
        /// <summary>
        /// 授予角色权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool GrantRolePermission(GrantRoleRequest request)
        {
            List<tbRolePermission> list = new List<tbRolePermission>();
            foreach (long ModulePermissionID in request.ModulePermissionIDs)
            {
                list.Add(new tbRolePermission()
                {
                    RoleID = request.RoleID,
                    CreateDate = DateTime.Now,
                    CreateUserID = SessionHelper.Get("UserID").ObjToIntNull(),
                    ModifyDate = DateTime.Now,
                    ModifyUserID = SessionHelper.Get("UserID").ObjToIntNull(),
                    ModulePermissionID = ModulePermissionID

                });
            }

          return  myDao.GrantRolePermission(list, request.RoleID);
        }

    }
}
