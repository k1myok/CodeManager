/*  作者：       tianzh
*  创建时间：   2012/8/4 15:49:40
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TZHSWEET.Common;
namespace TZHSWEET.ViewModel
{
    public class GrantRoleRequest
    {
        #region - 属性 -

        public string ModulePermissions { get; set; }
        
        public long[] ModulePermissionIDs { get; set; }
        public int RoleID { get; set; }
        #endregion

        #region - 构造函数 -

        public GrantRoleRequest(HttpContextBase context)
        {
            ModulePermissions = context.Request["ModulePermissions"];
            ModulePermissionIDs = GetPermissionIds(ModulePermissions);
            RoleID = context.Request["RoleID"].ObjToInt();
        }

        public GrantRoleRequest()
        {

        }

        #endregion

        #region - 方法 -

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public long[] GetPermissionIds(string permission)
        {
           
            string[] permissIDs = permission.Split(',');
             long[] ids=new long[permissIDs.Length-1];
            for (int i = 0; i < permissIDs.Length; i++)
            {
                if (!permissIDs[i].IsNullOrEmpty())
                {
                    ids[i] = permissIDs[i].ObjToLong();
                }
            }
            return ids;
        }

        #endregion


    }
}
