using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigData.ModelBuilding.Controllers
{
    public class ModelBuildingController : Controller
    {
        public PartialViewResult List()
        {
            ViewBag.Message = "测试viewbag";
            return PartialView();
        }
    }
}