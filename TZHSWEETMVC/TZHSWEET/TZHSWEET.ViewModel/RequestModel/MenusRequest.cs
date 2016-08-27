 /*  作者：       tianzh
 *  创建时间：   2012/7/25 15:07:22
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TZHSWEET.Entity;
using TZHSWEET.Common;

namespace TZHSWEET.ViewModel
{
   public class MenusRequest
    {
       public string MenuName { get; set; }
       public string MenuNo { get; set; }
       public string Controller { get; set; }
       public string MenuUrl { get; set; }
       public string MenuIcon { get; set; }
       public string MenuParentNo { get; set; }
       public bool IsVisible { get; set; }
       public bool IsDeleted { get; set; }
       public tbModule Module { get; set; }
       public int ModuleID { get; set; }
       public bool IsMenu { get; set; }
       public MenusRequest(HttpContextBase context)
       {
           MenuName = context.Request["MenuName"];
           MenuNo =context.Request["MenuNo"];
           MenuParentNo = context.Request["MenuParentNo"];
           Controller = context.Request["Controller"];
           MenuUrl = context.Request["MenuUrl"];
           IsVisible =context.Request["IsVisible"]=="1";
           IsDeleted = context.Request["IsDeleted"]=="1";
           IsMenu = context.Request["IsMenu"] == "1";
           MenuIcon =context.Request["MenuIcon"];
           ModuleID =Convert.ToInt32(context.Request["ModuleID"]);
           //判断路径
           if (!MenuIcon.IsNullOrEmpty())
             MenuIcon=MenuIcon[0] != '/' ? ("/" + MenuIcon) : MenuIcon;
           //module赋值
           Module = new tbModule();
           Module.IsMenu = IsMenu;
           Module.ModuleController = Controller;
           Module.ModuleIcon = MenuIcon;
           Module.ModuleLinkUrl = MenuUrl;
           Module.ModuleName = MenuName;
           Module.ParentNo =Convert.ToInt32(MenuParentNo);
           Module.ModuleID = ModuleID;
           //Module.Sort = 10;
           Module.IsDeleted = IsDeleted;
           Module.IsVisible = IsVisible;
       }
    }
}
