
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;

namespace TZHSWEET.IBLL
{
   public interface IRoleService : IBaseService<tbRole>
    {
        /// <summary>
        /// 根据条件获取所有菜单
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <returns></returns>
       LigerUIGrid GetAllRoles(LigerUIGridRequest request);
        /// <summary>
        /// 根据条件获取所有角色
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <param name="Count">记录总数</param>
        /// <returns></returns>
        IEnumerable<tbRole> GetAllRoles(LigerUIGridRequest request, out int Count);
         /// <summary>
        /// 获取角色的Select数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<LigerUISelect> GetRolesForSelect(LigerUISelectRequest request);
         /// <summary>
        /// 请求角色树形
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<LigerUITree> GetRoleTree(LigerUITreeRequest request);
    }
}
