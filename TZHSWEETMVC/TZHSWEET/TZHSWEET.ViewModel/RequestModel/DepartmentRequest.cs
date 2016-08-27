 /*  作者：       tianzh
 *  创建时间：   2012/7/20 22:48:02
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using System.Web;

namespace TZHSWEET.ViewModel
{
    /// <summary>
    /// 部门请求请求
    /// </summary>
    public class DepartmentRequest
    {
        public tbDepartment Department { get; set; }
        /// <summary>
        /// 实例化(增加和删除的请求构造)
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="IsUpdate">是否为更新的请求</param>
        public DepartmentRequest(HttpContextBase context,int UserID,bool IsUpdate)
        {
            Department = new tbDepartment();
            Department.DeptID = 0;//默认值为0
            //如果是更新请求
            if (IsUpdate)
            {
                Department.DeptID = Convert.ToInt32(context.Request["DeptID"]);
                Department.ModifyDate = DateTime.Now;
                Department.ModifyUserID = UserID;
            }
            Department.DeptName = context.Request["DeptName"];
            Department.DeptDescription = context.Request["DeptDesc"];
            Department.ParentID =Convert.ToInt32(context.Request["deptParentID"]);
            Department.IsDeleted = false;
            Department.CreateDate = DateTime.Now;
            Department.CreateUserID = UserID;
        }
        /// <summary>
        /// 删除的部门请求构造
        /// </summary>
        /// <param name="context"></param>
        /// <param name="UserID"></param>
        public DepartmentRequest(HttpContextBase context)
        {
            Department = new tbDepartment();
            Department.DeptID =Convert.ToInt32(context.Request["ID"]);//默认值为0
        }
       
    }
}
