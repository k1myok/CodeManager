 /*  作者：       tianzh
 *  创建时间：   2012/7/23 20:38:04
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TZHSWEET.Entity;

namespace TZHSWEET.ViewModel
{
   public  class FavoriteRequest
    {
       public string MenuNo { get; set; }
       public string FavoriteContent { get; set; }
       public tbFavorite Favorite { get; set; }

       public FavoriteRequest(HttpContextBase context)
       {
           MenuNo = context.Request["MenuNo"];
           FavoriteContent = context.Request["FavoriteContent"];
           Favorite = new tbFavorite();
           Favorite.FavoriteAddTime = DateTime.Now;
           Favorite.FavoriteContent = FavoriteContent;
       }
       /// <summary>
       /// 获取我的收藏的模块信息
       /// </summary>
       /// <param name="module"></param>
       public void GetFavoriteModuleInfo(tbModule module)
       {
           Favorite.Url = module.ModuleLinkUrl;
           Favorite.Icon = module.ModuleIcon;
           Favorite.FavoriteTitle = module.ModuleName;
       }
    }
}
