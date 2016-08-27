 /*  作者：       tianzh
 *  创建时间：   2012/8/3 20:33:42
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;

namespace TZHSWEET.ViewModel
{
   public class ViewModelRolePermission
    {
       public int PermissionID { get; set; }
       public string PermissionAction { get; set; }
       public string PermissionName { get; set; }
       public string Icon { get; set; }
       public string PermissionController { get; set; }
       public string Description { get; set; }
       public int? ParentID { get; set; }
       public string ModuleController { get; set; }
       public string ModuleName { get; set; }
       public string ModuleIcon { get; set; }
       public int? RoleID { get; set; }
       public long? ModulePermissionID { get; set; }
       public bool IsChecked { get; set; }
       public List<ViewModelRolePermission> children { get; set; }
       public static ViewModelRolePermission ToViewModel(VRoleGrantPermission permission)
       {
           ViewModelRolePermission item = new ViewModelRolePermission();
           item.PermissionID = permission.PermissionID;
           item.PermissionAction = permission.PermissionAction;
           item.PermissionName = permission.PermissionName;
           item.Icon = permission.Icon;
           item.PermissionController = permission.PermissionController;
           item.Description = permission.Description;
           item.ParentID = permission.ParentID;
           item.ModuleController = permission.ModuleController;
           item.ModuleName = permission.ModuleName;
           item.ModuleIcon = permission.ModuleIcon;
           item.ModulePermissionID = permission.ModulePermissionID;
           item.IsChecked = false;
           return item;

       }
    }
}
