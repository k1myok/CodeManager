using LYZJ.HM3Shop.BLL;
using LYZJ.HM3Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYZJ.HM3Shop.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        IBLL.IUserInfoService _userInfoService = new UserInfoService();

        public ActionResult Index()
        {
            UserInfo uInfo = Session["UserInfo"] as UserInfo;
            if (uInfo != null)
            {
                ViewBag.UName = uInfo.UName;
            }
            return View();
        }

        /// <summary>
        /// 查询出数据显示在菜单栏目中
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadMenuData()
        {
            var data = _userInfoService.LoadMenuData(CurrentUserInfo.ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
