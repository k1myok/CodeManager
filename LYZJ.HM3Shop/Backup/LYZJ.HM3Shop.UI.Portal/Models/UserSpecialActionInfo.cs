using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYZJ.HM3Shop.UI.Portal.Models
{
    /// <summary>
    /// 封装一个类封装所有需要的字段
    /// </summary>
    public class UserSpecialActionInfo
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int ActionID { get; set; }
        
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserID { get; set; }
        
        /// <summary>
        /// 是否允许访问
        /// </summary>
        public bool HasPermation { get; set; }
        
        /// <summary>
        /// 权限地址
        /// </summary>
        public string ActionURL { get; set; }

        public string actionInfoID { get; set; }
    }
}