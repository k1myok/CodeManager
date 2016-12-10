using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceBIA.Case.SZ.CityManager.Controllers
{
    public class EPController : Controller
    {
        public ActionResult SingleForm(Guid id)
        {
            return View(id);
        }
    }
}