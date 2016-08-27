 /*  作者：       tianzh
 *  创建时间：   2012/7/21 22:01:35
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.UI;
using System.ComponentModel;
using TZHSWEET.Common;

namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
   
    [Description("网站信息管理")]
    public class SystemInfoController : BaseController
    {
        //
        // GET: /Admin/WebInfo/
        [Description("网站信息管理")]
        [DefaultPage]
        [ViewPage]
        public ActionResult Index()
        {

           // UserOperateLog.WriteOperateLog("[浏览网站信息管理]" + SysOperate.Load.ToMessage(true));
            return View();
        }

    }
}
