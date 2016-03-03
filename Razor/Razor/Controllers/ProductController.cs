using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razor.Models;//手动添加的


namespace Razor.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {


            Product myProduct = new Product { ProductID=1,Name="Ken",Description="A boat for one person",Category="Watersports",Price=275M
            };
            return View(myProduct);
        }

    }
}
