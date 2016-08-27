 /*  作者：       tianzh
 *  创建时间：   2012/7/20 8:58:44
 *
 */
 /*  作者：       tianzh
 *  创建时间：   2012/7/19 22:15:05
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TZHSWEET.UI
{
    /// <summary>
    /// 表示当前Action请求为一个具体的功能页面
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ViewPageAttribute : Attribute
    {
    }
}