 /*  作者：       tianzh
 *  创建时间：   2012/7/20 17:08:04
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using Newtonsoft.Json;
using System.Web;

namespace TZHSWEET.ViewModel
{
 
    /// <summary>
    /// 按钮数据类
    /// </summary>
    public class ViewModelMyButton
    {
        /// <summary>
        /// 按钮ID
        /// </summary>
        public string BtnNo { get; set; }
        /// <summary>
        /// 按钮功能名称
        /// </summary>
        public string BtnName { get; set; }
        /// <summary>
        /// 按钮图标地址
        /// </summary>
        public string BtnIcon { get; set; }
        /// <summary>
        /// 点击事件(暂时停用)
        /// </summary>
        public string BtnClick { get; set; }
        /// <summary>
        /// 菜单标识
        /// </summary>
        public string MenuNo { get; set; }
     
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
  new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, });

        }
        /// <summary>
        /// 转化为按钮实体
        /// </summary>
        /// <param name="menu"></param>
        public static ViewModelMyButton ToEntity(VRoleModulePermission menu)
        {
            ViewModelMyButton item = new ViewModelMyButton();

            item.BtnNo = menu.PermissionAction;
            item.BtnName = menu.PermissionName;
            item.BtnIcon = menu.Icon;
            item.BtnClick = menu.Script;
            return item;
        }
        
    }
}
