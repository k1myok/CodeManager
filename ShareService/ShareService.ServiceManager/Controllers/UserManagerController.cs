using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;

namespace ShareService.ServiceManager.Controllers
{
    public class UserManagerController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();

        // GET: UserManager
        public PartialViewResult List()
        {
            return PartialView(context.UFUser);
        }

        public PartialViewResult Create()
        {
            var model = new UFUser();
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(UFUser model)
        {
            model.Code = Guid.NewGuid();
            context.UFUser.Add(model);
            var result = context.SaveChanges() > 0;
            return Json(new { State = result });
        }

        public PartialViewResult Edit(Guid code)
        {
            var model = context.UFUser.Find(code);
            model.Password = null;
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Edit(UFUser model)
        {
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            var result = context.SaveChanges() > 0;
            return Json(new { State = result });
        }

        public PartialViewResult Detail(Guid code)
        {
            var model = context.UFUser.Find(code);
            model.Password = null;
            return PartialView(model);
        }

        public JsonResult Delete(Guid code)
        {
            var model = context.UFUser.Find(code);
            if(model == null)
                return Json(new { State = false });

            context.UFUser.Remove(model);
            var result = context.SaveChanges() > 0;
            return Json(new { State = result }, JsonRequestBehavior.AllowGet);
        }
    }
}