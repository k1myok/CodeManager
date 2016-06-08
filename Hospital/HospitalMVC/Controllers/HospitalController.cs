using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalData;

namespace HospitalMVC.Controllers
{
    public class HospitalController : Controller
    {
        //
        // GET: /Hospital/
        private CityServiceEntities content = new CityServiceEntities();
        public ActionResult Index()
        {
            return View(content.Hospitals.Distinct());
        }
        public PartialViewResult GetDepart(string Hosp)
        {
            var result = content.Departs.Where(p=>p.HospName==Hosp);
            return PartialView(result);
        }

    }
}
