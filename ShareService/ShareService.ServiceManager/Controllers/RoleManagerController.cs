using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;

namespace ShareService.ServiceManager.Controllers
{
    public class RoleManagerController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();

        // GET: UserManager
        public PartialViewResult List()
        {
            return PartialView(context.UFRole);
        }

        public PartialViewResult Create()
        {
            var model = new UFRole();
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(UFRole model)
        {
            model.Code = Guid.NewGuid();
            context.UFRole.Add(model);
            var result = context.SaveChanges() > 0;
            return Json(new { State = result });
        }

        public PartialViewResult Edit(Guid code)
        {
            var model = context.UFRole.Find(code);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Edit(UFRole model)
        {
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            var result = context.SaveChanges() > 0;
            return Json(new { State = result });
        }

        public PartialViewResult Detail(Guid code)
        {
            var model = context.UFRole.Find(code);
            return PartialView(model);
        }

        public JsonResult Delete(Guid code)
        {
            var model = context.UFRole.Find(code);
            if (model == null)
                return Json(new { State = false });

            context.UFRole.Remove(model);
            var result = context.SaveChanges() > 0;
            return Json(new { State = result }, JsonRequestBehavior.AllowGet);
        }
    }
}