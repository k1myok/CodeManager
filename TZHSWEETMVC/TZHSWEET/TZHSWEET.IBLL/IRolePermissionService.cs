
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;

namespace TZHSWEET.IBLL
{
   public interface IRolePermissionService : IBaseService<tbRolePermission>
    {
        /// <summary>
        /// 获取角色权限Grid信息(如果数据量大需要考虑用异步的GridTree)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        LigerUIGrid GetPermissionForGridTree(LigerUIGridRequest request);
         /// <summary>
        /// 获取所有角色权限的json格式
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
       object GetAllRolePermission(LigerUIGridRequest grid);
        /// <summary>
        /// 授予角色权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
      bool GrantRolePermission(GrantRoleRequest request);
    }
}
