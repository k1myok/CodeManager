/*  作者：       tianzh
*  创建时间：   2012/7/18 23:56:55
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TZHSWEET.ViewModel
{
    public class ViewModelMenu
    {
        #region - 属性 -

        /// <summary>
        /// 前四个是ligerui必须参数(大小写也要一样)
        /// </summary>
        public string icon { get; set; }

        public int id { get; set; }

        public string text { get; set; }

        public List<ViewModelMenu> children { get; set; }

        public string MenuUrl { get; set; }

        public int ModuleID { get; set; }

        public string MenuName { get; set; }

        public string MenuIcon { get; set; }

        public string MenuNo { get; set; }

        public string MenuParentNo { get; set; }

        public string Controller { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsMenu { get; set; }
        #endregion

        #region - 方法 -

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
  new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }

        #endregion

        #region Filter翻译机 where条件转化为数据库参数
        /// <summary>
        /// Filter翻译机过滤组件对where中UI中LigerUIMenu到tbModule部分实体进行转换
        /// </summary>
        /// <param name="where">由Filter过来的where条件</param>
        /// <returns></returns>
        public static string FilterParamsToEntityParams(string where)
        {

            where = where.Replace("MenuNo", "ModuleID");
            where = where.Replace("MenuParentNo", "ParentNo");
            where = where.Replace("Controller", "ModuleController");
            return where;
        }
        #endregion
        #region ToEntity
        /// <summary>
        /// 转化为菜单实体
        /// </summary>
        /// <param name="department"></param>
        public static ViewModelMenu ToEntity(VRoleMenuPermission menu)
        {
            ViewModelMenu item = new ViewModelMenu();
            item.icon = menu.ModuleIcon;
            item.id = Convert.ToInt32(menu.ModuleID);
            item.text = menu.ModuleName;
            // this.Children = new List<Menu>();
            item.MenuName = menu.ModuleName;
            item.MenuIcon = menu.ModuleIcon;
            item.MenuNo = menu.ModuleID.ToString ();
            item.MenuUrl = menu.ModuleLinkUrl;
            item.MenuParentNo = menu.ParentNo.Value.ToString ();
            item.Controller = menu.ModuleController;
            item.IsVisible = menu.IsVisible.Value;
            item.IsDeleted = menu.IsDeleted.Value;
            item.ModuleID = menu.ModuleID;
            item.IsMenu = menu.IsMenu.Value;
            return item;
        }
        /// <summary>
        /// 转化为菜单实体
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static ViewModelMenu ToEntity(tbModule module)
        {
            ViewModelMenu item = new ViewModelMenu();
            item.icon = module.ModuleIcon;
            item.id = Convert.ToInt32(module.ModuleID);
            item.text = module.ModuleName;
            // this.Children = new List<Menu>();
            item.MenuName = module.ModuleName;
            item.MenuIcon = module.ModuleIcon;
            item.MenuNo = module.ModuleID.ToString ();
            item.MenuUrl = module.ModuleLinkUrl;
            item.MenuParentNo = module.ParentNo.Value.ToString ();
            item.Controller = module.ModuleController;
            item.IsVisible = module.IsVisible.Value;
            item.IsDeleted = module.IsDeleted.Value;
            item.ModuleID = module.ModuleID;
            item.IsMenu = module.IsMenu.Value;
            return item;
        }
        #endregion
        #region ToViewModel
        /// <summary>
        /// 转化为ViewModel的list集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<ViewModelMenu> ToListViewModel(IEnumerable<VRoleMenuPermission> list)
        {
            var menuList = new List<ViewModelMenu>();
            foreach (VRoleMenuPermission item in list)
            {
                menuList.Add(ViewModelMenu.ToEntity(item));
            }
            return menuList;
        }
        /// <summary>
        /// 转化为ViewModel的list集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<ViewModelMenu> ToListViewModel(IEnumerable<tbModule> list)
        {
            var menuList = new List<ViewModelMenu>();
            foreach (tbModule item in list)
            {
                menuList.Add(ViewModelMenu.ToEntity(item));
            }
            return menuList;
        }
        #endregion
    }
}
