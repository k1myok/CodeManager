using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Razor.Models
{
    //public class Product : Controller
    //{
    //    //
    //    // GET: /Product/

    //    //public ActionResult Index()
    //    //{
    //    //    return View();
    //    //}
       
    //}
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }


    }

}
