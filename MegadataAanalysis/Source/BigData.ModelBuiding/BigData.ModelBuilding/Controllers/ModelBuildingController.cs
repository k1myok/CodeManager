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
            return PartialView(context.BuildingModel);
        }
        public PartialViewResult ListDetail(Guid code)
        {
            var result = context.BuildingModel.FirstOrDefault(p => p.Code == code);
            return PartialView(result);
        }
        public PartialViewResult GetList(Guid code)
        {
            var result = context.AnalysisModel.Where(p => p.Code == code).Select(am => new SelectListItem { Value=am.Name,Text=am.Name}).ToList();
            ViewBag.result = result;
            return PartialView();
        }
    }
}