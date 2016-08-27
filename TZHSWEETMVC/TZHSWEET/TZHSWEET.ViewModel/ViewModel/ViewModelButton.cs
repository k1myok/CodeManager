/*  作者：       tianzh
*  创建时间：   2012/7/26 15:52:19
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
    public class ViewModelButton
    {
        #region - 属性 -

        public int ID { get; set; }

        public string Action { get; set; }

        public string Name { get; set; }

        public bool IsVisible { get; set; }

        public string Script { get; set; }

        public string Icon { get; set; }

        public string Controller { get; set; }

        public string Description { get; set; }
        public bool IsButton { get; set; }
        public int ParentID { get; set; }
        public List<ViewModelButton> children { get; set; }
        #endregion

        #region - 构造函数 -

        public ViewModelButton()
        {

        }

        public ViewModelButton(HttpContextBase context)
        {
            ID = Convert.ToInt32(context.Request["ID"]);
            Action = context.Request["Action"];
            Name = context.Request["Name"];
            IsVisible = context.Request["IsVisible"] == "1";
            Script = context.Request["Script"];
            Icon = context.Request["Icon"];
            this.Controller = context.Request["Controller"];
            IsButton = context.Request["IsButton"]=="1";
            ParentID = context.Request["ParentID"].ObjToInt();
        }

        #endregion

        #region - 方法 -
        #region Filter翻译机 where条件转化为数据库参数
        /// <summary>
        /// Filter翻译机过滤组件对where中UI中ViewModelButton到tbPermission部分实体进行转换
        /// </summary>
        /// <param name="where">由Filter过来的where条件</param>
        /// <returns></returns>
        public static string FilterParamsToEntityParams(string where)
        {

            where = where.Replace("ID", "PermissionID");
            where = where.Replace("Action", "PermissionAction");
            where = where.Replace("Controller", "PermissionController");
            where = where.Replace("Name", "PermissionName");
            return where;
        }
        #endregion
        #region ToEntity
        public static tbPermission ToEntity(ViewModelButton permission)
        {
            tbPermission item = new tbPermission();
            item.PermissionID = permission.ID;
            item.PermissionName = permission.Name;
            item.PermissionAction = permission.Action;
            item.Script = permission.Script;
            item.Icon = permission.Icon;
            item.IsVisible = permission.IsVisible;
            item.PermissionController = permission.Controller;
            item.Description = permission.Description;
            item.IsButton = permission.IsButton;
            item.ParentID = permission.ParentID;
            return item;
        }

        #endregion
        #region ToViewModel
        /// <summary>
        /// 转化为ViewModel
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static ViewModelButton ToViewModel(tbPermission permission)
        {
            ViewModelButton item = new ViewModelButton();
            item.ID = permission.PermissionID;
            item.Name = permission.PermissionName;
            item.Action = permission.PermissionAction;
            item.IsVisible = permission.IsVisible.Value;
            item.Icon = permission.Icon;
            item.Script = permission.Script;
            item.Controller = permission.PermissionController;
            item.Description = permission.Description;
            item.IsButton = permission.IsButton.Value;
            item.ParentID = permission.ParentID.Value;
            return item;
        }

        /// <summary>
        /// 转化为List集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<ViewModelButton> ToListViewModel(IEnumerable<tbPermission> list)
        {
            var listModel = new List<ViewModelButton>();
            foreach (tbPermission item in list)
            {
                listModel.Add(ViewModelButton.ToViewModel(item));
            }
            return listModel;
        } 
        #endregion

        #endregion


    }
}
