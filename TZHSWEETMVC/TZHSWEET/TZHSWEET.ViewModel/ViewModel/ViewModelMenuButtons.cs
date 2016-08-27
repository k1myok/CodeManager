/*  作者：       tianzh
*  创建时间：   2012/7/27 16:33:59
*
*/
/*  作者：       tianzh
*  创建时间：   2012/7/27 10:42:16
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using System.Web;
using TZHSWEET.Common;
namespace TZHSWEET.ViewModel
{
    public class ViewModelMenuButtons
    {
        #region - 属性 -

        public long ModulePermissionID { get; set; }

        public int? MenuID { get; set; }

        public int? ButtonID { get; set; }

        public string ButtonName { get; set; }

        public string MenuName { get; set; }

        public string MenuUrl { get; set; }

        public string MenuController { get; set; }

        public string ButtonAction { get; set; }

        public string ButtonIcon { get; set; }

        public string MenuNo { get; set; }

        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        #endregion

        #region - 构造函数 -

        public ViewModelMenuButtons()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="IsAdd"></param>
        public ViewModelMenuButtons(HttpContextBase context, bool IsAdd)
        {
            MenuID = context.Request["MenuID"].ObjToIntNull();
            ButtonID = context.Request["ButtonID"].ObjToIntNull();
            ButtonName = context.Request["ButtonName"];
            ButtonIcon = context.Request["ButtonIcon"];
            ButtonAction = context.Request["ButtonAction"];
            MenuController = context.Request["MenuController"];
            IsDeleted = context.Request["IsDeleted"] == "1";
            if (!IsAdd)
            {
                ModulePermissionID = context.Request["ModulePermissionID"].ObjToLong();
            }

        }

        #endregion

        #region - 方法 -

        /// <summary>
        ///  转化为ViewModel实体
        /// </summary>
        /// <param name="vPermission"></param>
        /// <returns></returns>
        public static ViewModelMenuButtons ToViewModel(VModulePermission vPermission)
        {
            ViewModelMenuButtons item = new ViewModelMenuButtons();
            item.ModulePermissionID = vPermission.ModulePermissionID;
            item.MenuUrl = vPermission.MenuUrl;
            item.MenuName = vPermission.MenuName;
            item.MenuID = vPermission.MenuID;
            item.MenuController = vPermission.MenuController;
            item.ButtonName = vPermission.ButtonName;
            item.ButtonAction = vPermission.ButtonAction;
            item.ButtonID = vPermission.ButtonID;
            item.ButtonIcon = vPermission.ButtonIcon;
            item.MenuNo = vPermission.MenuID.Value.ToString ();
            item.IsDeleted = vPermission.modPermissionIsDeleted.Value;
            item.Description = vPermission.Description;
            item.ParentID = vPermission.ParentID;
            return item;
        }
        /// <summary>
        /// ToEntity实体对象(保存到数据库的实体对象)
        /// </summary>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static tbModulePermission ToEntity(ViewModelMenuButtons buttons)
        {
            tbModulePermission item = new tbModulePermission();
            item.ModulePermissionID = buttons.ModulePermissionID;
            item.ModuleID = buttons.MenuID.Value;
            item.PermissionID = buttons.ButtonID.Value;
            item.CreateDate = DateTime.Now;
            item.CreateUserID = SessionHelper.Get("UserID").ObjToIntNull();
            item.ModifyUserID = SessionHelper.Get("UserID").ObjToIntNull();
            item.ModifyDate = DateTime.Now;
            item.IsDeleted = buttons.IsDeleted;
            return item;
        }
        /// <summary>
        /// 转化为ViewModel 的list集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<ViewModelMenuButtons> ToListViewModel(IEnumerable<VModulePermission> list)
        {

            var listModel = new List<ViewModelMenuButtons>();
            foreach (VModulePermission item in list)
            {
                listModel.Add(ViewModelMenuButtons.ToViewModel(item));
            }
            return listModel;
        }

        #endregion


    }
}
