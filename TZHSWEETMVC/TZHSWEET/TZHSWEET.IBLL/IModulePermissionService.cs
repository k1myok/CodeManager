
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;

namespace TZHSWEET.IBLL
{
   public interface IModulePermissionService : IBaseService<tbModulePermission>
    {
        /// <summary>
        /// 获取菜单的按钮信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        IEnumerable<VModulePermission> GetMenuButtons(LigerUIGridRequest request, out int Count);
      /// <summary>
        /// 获取菜单的按钮信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        LigerUIGrid GetMenuButtons(LigerUIGridRequest request);
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="buttons">ViewModel菜单按钮模型</param>
        /// <returns></returns>
        bool Insert(ViewModelMenuButtons buttons);
         /// <summary>
        /// 修改
        /// </summary>
        /// <param name="buttons">ViewModel菜单按钮模型</param>
        /// <returns></returns>
        bool Update(ViewModelMenuButtons buttons);
    }
}
