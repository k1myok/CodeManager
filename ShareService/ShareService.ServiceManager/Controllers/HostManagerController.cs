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
    public class HostManagerController : Controller
    {
        // GET: HostManager
        private ShareServiceContext context = new ShareServiceContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Manager()
        {
            return PartialView();
        }

        public PartialViewResult FarmsList()
        {
            return PartialView(context.ServerFarm);
        }

        public PartialViewResult HostsList(Guid farmCode)
        {
            ViewBag.FarmCode = farmCode;
            return PartialView(context.ServerInstance.Where(p => p.FarmCode == farmCode));
        }

        public PartialViewResult CreateFarm()
        {
            return PartialView(new ServerFarm());
        }
        public PartialViewResult CreateServerInstance(Guid farmCode)
        {
            return PartialView(new ServerInstance() { 
                 FarmCode = farmCode
            });
        }
        [HttpPost]
        public JsonResult CreateServerInstance(ServerInstance ServerIn)
        {
            ServerIn.Code = Guid.NewGuid();
            context.ServerInstance.Add(ServerIn);
            var result = context.SaveChanges() > 0;

            return Json(new
            {
                State = result
            });
        }
       
        public JsonResult CreateFarm(ServerFarm farm)
        {
            farm.Code = Guid.NewGuid();
            context.ServerFarm.Add(farm);
            var result = context.SaveChanges() > 0;

            return Json(new {
                State = result
            });
        }
        public JsonResult DeleteServerInstance(Guid code)
        {
            var target = context.ServerInstance.Find(code);
            if (target != null)
            {
                context.ServerInstance.Remove(target);
                context.SaveChanges();
                return Json(new
                {
                    State = true
                });
            }
            return Json(new
            {
                State = false
            });
        }

        public PartialViewResult EditServerInstance(Guid code)
        {
            var target = context.ServerInstance.Find(code);
            return PartialView(target);
        }

        [HttpPost]
        public JsonResult EditServerInstance(ServerInstance model)
        {
            if(ModelState.IsValid)
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

    }
}