 /*  作者：       tianzh
 *  创建时间：   2012/7/21 14:59:24
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TZHSWEET.UI
{
    /// <summary>
    /// 代表该方法可以允许登录用户都能访问
    /// </summary>
     [AttributeUsage(AttributeTargets.Method)]
   public class LoginAllowViewAttribute:Attribute
    {
    }
}
