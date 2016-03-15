using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigData.ModelBuilding.DAL;
using BigData.ModelBuilding.Models;

namespace BigData.ModelBuilding.Controllers
{
    public class ModelBuildingController : Controller
    {
        private ModelBuildingContext context = new ModelBuildingContext();
        public PartialViewResult List()
        {
            return PartialView(context.BaseField);
        }
    }
}