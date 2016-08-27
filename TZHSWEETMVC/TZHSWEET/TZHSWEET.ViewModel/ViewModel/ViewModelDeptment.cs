 /*  作者：       tianzh
 *  创建时间：   2012/7/21 16:22:42
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;

namespace TZHSWEET.ViewModel
{
    /// <summary>
    /// View UI视图的模型
    /// </summary>
   public class ViewModelDeptment
    {
       public int DeptID { get; set; }
       public string DeptName { get; set; }
       public string DeptDesc { get; set; }
       public int? ParentID { get; set; }
       /// <summary>
       /// 转化为ViewModel实体
       /// </summary>
       /// <param name="depart"></param>
       /// <returns></returns>
       public static ViewModelDeptment ToViewModel(tbDepartment department)
       {
           ViewModelDeptment item = new ViewModelDeptment();
           item.DeptID = department.DeptID;
           item.DeptName = department.DeptName;
           item.DeptDesc = department.DeptDescription;
           item.ParentID = department.ParentID;
           return item;
       }
    }
}
