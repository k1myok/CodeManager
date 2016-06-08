using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;

namespace ShareService.ServiceManager.Controllers
{
    [UFAuthorize]
    public class MetadataManagerController : Controller
    {
        public ActionResult Create()
        {
            var types = new List<string>() {
                "字符串",
                "数值"
            };

            ViewBag.FieldTypes = types.Select(p => new SelectListItem() { Text = p, Value = p });

            var model = new BaseMetadata() {};
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(BaseMetadata model)
        {
            using (var context = new ShareServiceContext())
            {               
                model.Code = Guid.NewGuid();
                context.BaseMetadata.Add(model);
                return Json(new {State= context.SaveChanges() > 0 });
            }
        }

        public ActionResult List() {
            return View();
        }

        public PartialViewResult Edit(Guid Code) {
            var types = new List<string>() {
                "字符串",
                "数值"
            };

            ViewBag.FieldTypes = types.Select(p => new SelectListItem() { Text = p, Value = p });

            var model = new ShareServiceContext().BaseMetadata.FirstOrDefault(b=>b.Code==Code);
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult Edit(BaseMetadata model) {
            var context = new ShareServiceContext();
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            var res = context.SaveChanges() > 0;
            return Json(new {result=res});
        }

        public JsonResult Delete(Guid Code) {
            var context = new ShareServiceContext();
            var target = context.BaseMetadata.FirstOrDefault(b=>b.Code==Code);
            context.Entry(target).State = System.Data.Entity.EntityState.Deleted;
            return Json(new {result=context.SaveChanges()>0 });
        }

        public PartialViewResult GetMetadataList() {
            var context = new ShareServiceContext();
            return PartialView(context.BaseMetadata.ToList());
        }
    }
}