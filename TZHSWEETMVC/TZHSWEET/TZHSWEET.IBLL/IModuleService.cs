
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;

namespace TZHSWEET.IBLL
{
   public interface IModuleService : IBaseService<tbModule>
    {
      /// <summary>
        /// 根据条件获取所有菜单
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <param name="Count">记录总数</param>
        /// <returns></returns>
       IEnumerable<tbModule> GetAdminMenus(LigerUIGridRequest request, out int Count);
     
    }
}
