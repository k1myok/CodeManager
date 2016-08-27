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
    public class DepartmentService : BaseService<tbDepartment>, IDepartmentService
    { 
         /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IDepartmentDao<tbDepartment> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public DepartmentService()
        {
			//spring.net注入
        IApplicationContext ctx = ContextRegistry.GetContext();
      
            myDao = ctx.GetObject("DepartmentDao") as IDepartmentDao<tbDepartment>;
            // DaoFactory.GetDepartmentDao();
            Dao = myDao;
        }
      
        /// <summary>
        /// 获取树形的Select数据(暂时没有任何处理)
        /// </summary>
        /// <param name="selectData"></param>
        /// <returns></returns>
        public IEnumerable<tbDepartment> GetAllDepartmentSelect(LigerUISelectRequest selectData)
        {
         return  myDao.GetEntities(p=>true);
        }
        /// <summary>
        /// 获取树形的Select的json数据
        /// </summary>
        /// <param name="selectData"></param>
        /// <returns></returns>
        public IEnumerable<LigerUISelect> GetAllDepartmentSelectToViewModel(LigerUISelectRequest selectData)
        {
            IEnumerable<tbDepartment> list = GetAllDepartmentSelect(selectData);
            return LigerUISelect.ToListModel(list);

        }
        #region 获取部门GridTree的json格式数据
        /// <summary>
        /// 获取部门的Tree格式
        /// </summary>
        /// <param name="treeData">获得树级请求数据</param>
        /// <returns></returns>
        public IEnumerable<LigerUITree>  GetDepartmentTree(LigerUITreeRequest treeData)
        {
            StringBuilder sbJson = new StringBuilder();
            IEnumerable<tbDepartment> departAllList = myDao.GetEntities(p=>true);
            List<LigerUITree> listDepart = new List<LigerUITree>();
            //查找所有的一级部门
            var ParentDepart = departAllList.Where(con => con.ParentID.Value == 0);

            foreach (var parent in ParentDepart)
            {
                //实体转化 
                LigerUITree parentItem = LigerUITree.ToEntity(parent);
                //获取子级
                GetDepartmentChildren(ref parentItem, (List<tbDepartment>)departAllList);
                listDepart.Add(parentItem);
            }
            return listDepart;
           
        }

        #region 获取部门GridTree的json格式数据
        /// <summary>
        /// 获取部门GridTree的json格式数据
        /// </summary>
        /// <returns></returns>
        public string GetDepartmentGridTree(LigerUIGridRequest gridData)
        {
            int total = 0;
            IEnumerable<tbDepartment> departAllList =GetAllEntitiesByPaging(gridData, out total);
            List<LigerUITree> listDepart = new List<LigerUITree>();
            //查找所有的一级部门
            var ParentDepart = departAllList.Where(con => con.ParentID.Value == 0);


            foreach (var parent in ParentDepart)
            {
                //实体转化 
                LigerUITree parentItem = LigerUITree.ToEntity(parent);
                //获取子级
                GetDepartmentChildren(ref parentItem, (List<tbDepartment>)departAllList);
                listDepart.Add(parentItem);

            }
            //grid数据输出
            LigerUIGrid grid = new LigerUIGrid();
            grid.Rows = listDepart;
            grid.Total = total;
            return JsonHelper.ToJson(grid, true);

        } 
        #endregion
        /// <summary>
        /// 获取父级部门下的子部门列表信息
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="allList"></param>
        /// <returns></returns>
        private void GetDepartmentChildren(ref LigerUITree parent, List<tbDepartment> allList)
        {
            foreach (tbDepartment depart in allList)
            {

                if (depart.ParentID == parent.id)
                {

                    //实体转化
                    LigerUITree child = LigerUITree.ToEntity(depart);
                    if (parent.children == null)
                        parent.children = new List<LigerUITree>();
                    //添加到父级的Children中
                    parent.children.Add(child);
                    GetDepartmentChildren(ref child, allList);//递归添加子树
                }
            }
        }
        #endregion

    }
}
