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
            ViewBag.SystemModelSource = context.AnalysisModel.Select(p => new SelectListItem() { Text = p.Name, Value = p.Code.ToString() });

            var result = context.BuildingModel.FirstOrDefault(p => p.Code == code);
            return PartialView(result);
        }
        //public PartialViewResult GetFieldsInfo(Guid code)
        //{
        //    var result = context.AnalysisModel.Where(p => p.Code == code).Select(am => new SelectListItem { Value = am.Name, Text = am.Name }).ToList();
        //    ViewBag.result = result;
        //    return PartialView();
        //}

        public PartialViewResult CreateBasicInfoModel()
        {
            return PartialView(new BuildingModel());
        }
         [HttpPost]
        public JsonResult CreateBasicInfoModel(BuildingModel model)
        {
            model.CreateDate =Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.UpdateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
          model.Code = Guid.NewGuid();
          context.BuildingModel.Add(model);
          var result=context.SaveChanges()>0;
          return Json(new { Status=result });
        }
        [HttpPost]
        public JsonResult EditBasicInfoModel(BuildingModel model)
        {
            if (ModelState.IsValid)
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                var result = context.SaveChanges() > 0;
                return Json(new
                {
                    State = result
                });
            }
            else
            {
                return Json(new
                {
                    State = false
                });
            }
        }
        public PartialViewResult GetAnalysisModel()
        {
            var result = context.AnalysisModel.Select(am => new SelectListItem { Value =am.Code.ToString(), Text = am.Name }).ToList();
            ViewBag.result = result;
            return PartialView();
        }
        public JsonResult DeleteBasicInfoModel(Guid code)
        {
            var model = context.BuildingModel.Find(code);
            if (model != null)
            {
                context.BuildingModel.Remove(model);
                context.SaveChanges();
                return Json(new
                {
                    State = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                State = false
            },JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult FilesInfo(Guid code)
        {
            var baseFields =(from a in context.AnalysisModel
                             join b in context.AnalysisModelFieldsInfo
                             on a.Code equals b.ModelCode
                             join c in context.BaseField
                             on b.FieldCode equals c.Code
                             where a.Code == code
                             select c).ToList();
            return PartialView(baseFields);
        }
    }  
    }