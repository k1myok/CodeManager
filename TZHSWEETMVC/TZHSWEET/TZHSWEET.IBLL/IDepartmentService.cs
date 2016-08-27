
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;


namespace TZHSWEET.IBLL
{
   public interface IDepartmentService : IBaseService<tbDepartment>
    {
        /// <summary>
        /// 获取部门GridTree的json格式数据
        /// </summary>
        /// <returns></returns>
        string GetDepartmentGridTree(LigerUIGridRequest gridData);
         /// <summary>
        /// 获取树形的Select数据
        /// </summary>
        /// <param name="selectData"></param>
        /// <returns></returns>
        IEnumerable<tbDepartment> GetAllDepartmentSelect(LigerUISelectRequest selectData);
        /// <summary>
        /// 获取树形的Select的json数据
        /// </summary>
        /// <param name="selectData"></param>
        /// <returns></returns>
        IEnumerable<LigerUISelect> GetAllDepartmentSelectToViewModel(LigerUISelectRequest selectData);
       /// <summary>
        /// 获取部门的Tree格式
        /// </summary>
        /// <param name="treeData">获得树级请求数据</param>
        /// <returns></returns>
        IEnumerable<LigerUITree> GetDepartmentTree(LigerUITreeRequest treeData);
    }
}
