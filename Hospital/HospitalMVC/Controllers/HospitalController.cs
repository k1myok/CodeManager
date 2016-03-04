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

        public ActionResult Index()
        {
            return View();
        }
        public string[] GetHospitalList()
        {
            var context = new CityServiceEntities();
            var result = context.Hospitals.OrderBy(p => p.HospName).Select(p => p.HospName).ToArray();
            return result;
        }

    }
}
