
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;

namespace TZHSWEET.IBLL
{
   public interface IPermissionService : IBaseService<tbPermission>
    {
        /// <summary>
        /// 请求权限列表信息
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
       LigerUIGrid GetPermissionForGrid(LigerUIGridRequest request);
         /// <summary>
        /// 根据条件获取权限信息
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="Count">总个数</param>
        /// <returns></returns>
       IEnumerable<tbPermission> GetPermission(LigerUIGridRequest request, out int Count);
        /// <summary>
        /// 获取所有的权限列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<ViewModelButton> GetPermission(LigerUIGridRequest request);
         /// <summary>
        /// 判断是否存在该权限(根据控制器和动作)
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="action">动作</param>
        /// <returns></returns>
        bool IsPermissionExist(string controller, string action);
        /// <summary>
        /// 获得权限树
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        LigerUIGrid GetPermissionGridTree(LigerUIGridRequest request);
    }
}
