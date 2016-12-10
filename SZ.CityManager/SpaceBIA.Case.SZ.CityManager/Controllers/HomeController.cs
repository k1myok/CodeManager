using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceBIA.Case.SZ.CityManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ViewResult ReactSample()
        {
            return View();
        }
        public ViewResult KSReactSample()
        {
            return View();
        }
        public PartialViewResult PVSample()
        {
            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public PartialViewResult Empty()
        {
            
            return PartialView();
        }
        public PartialViewResult SwipeLayer()
        {
            return PartialView();
        }
    }
}