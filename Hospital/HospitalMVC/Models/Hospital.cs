using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVC.Models
{
    public class Hospital
    {
        //
        // GET: /Hospital/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public string HospName { get; set; }
        public string Grade { get; set; }
        public string Kind { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Intro { get; set; }

    }
}
