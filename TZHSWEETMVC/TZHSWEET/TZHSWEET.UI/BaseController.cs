 /*  作者：       tianzh
 *  创建时间：   2012/7/20 8:58:51
 *
 */
 /*  作者：       tianzh
 *  创建时间：   2012/7/19 21:34:13
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TZHSWEET.UI
{
    /// <summary>
    /// Admin后台系统公共控制器(需要验证的模块)
    /// </summary>
    [PermissionFilter]
    public class BaseController:Controller
    {

    }
}