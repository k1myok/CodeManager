 /*  作者：       tianzh
 *  创建时间：   2012/7/20 19:52:35
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics;

namespace TZHSWEET.ViewModel
{
    /// <summary>
    /// 获取按钮请求的类
    /// </summary>
    public class ButtonRequest
    {
        /// <summary>
        /// 菜单标识
        /// </summary>
        public string MenuNo { get; set; }
        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="context"></param>
        public ButtonRequest(HttpContextBase context)
        {
            MenuNo =context.Request["MenuNo"];
          
        }
    }
}
