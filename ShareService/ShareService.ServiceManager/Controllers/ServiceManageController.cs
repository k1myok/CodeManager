using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareService.ServiceManager.Controllers
{
    public class ServiceManageController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();


        #region weifeifei



        #endregion


        // GET: ServiceManage
        public ActionResult Index(int index = 0)
        {
            return View(index);
        }
        #region Farm
        public ActionResult Farms()
        {
            return View(context.ServerFarm);
        }
        [HttpPost]
        public ActionResult CreateFarm(ServerFarm serverFarm)
        {
            if (ModelState.IsValid)
            {
                serverFarm.Code = Guid.NewGuid();
                context.ServerFarm.Add(serverFarm);
                context.SaveChanges();
                return RedirectToAction("Index", new { index = 0 });
            }
            else {
                return Content("添加失败！");
            }
            
        }
        public ActionResult EditFarm(Guid guid)
        {
            var farm = context.ServerFarm.Find(guid);
            return View(farm);
        }
        [HttpPost]
        public ActionResult EditFarm(ServerFarm serviceFarm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Entry(serviceFarm).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index", new { index = 0 });
                }
                catch (Exception e)
                {
                    return Content("编辑失败！");
                }
            }
            else { return View(serviceFarm); }
        }
        public ActionResult DeleteFarm(Guid guid)
        {
            try
            {
                var target = context.ServerFarm.Find(guid);
                if (null == target) return RedirectToAction("Farms", new { index = 0 });
                context.Entry(target).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return RedirectToAction("Index", new { index = 0 });
            }
            catch (Exception e)
            {
                return Content("删除失败！");
            }
        }
        public ActionResult DetailFarm(Guid guid)
        {
            return Content("显示场的详细信息。");
        } 
        #endregion
        public ActionResult ServerInstances() {
            return View(context.ServerInstance);
        }
       
        [HttpPost]
        public ActionResult CreateServerInstance(ServerInstance serverInstance) {
            if (ModelState.IsValid) {
                var belongedFarmGUID = Guid.Parse(Request.Form["DisplayAllFarmsName"]);
                serverInstance.Code = Guid.NewGuid();
                serverInstance.FarmCode = belongedFarmGUID;
                context.Entry(serverInstance).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return RedirectToAction("Index", new { index = 1 });
            } else {
                return Content("添加失败！");
            }
        }
        public ActionResult DeleteServerInstance(Guid guid)
        {
            try
            {
                var target = context.ServerInstance.FirstOrDefault(sf => sf.Code == guid);
                if (null == target) return RedirectToAction("Farms", new { index = 0 });
                context.Entry(target).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return RedirectToAction("Index", new { index = 1 });
            }
            catch (Exception e)
            {
                return Content("删除失败！");
            }
        }
        public ActionResult EditServerInstance(Guid guid) {
            var serverInstance = context.ServerInstance.Find(guid);
            return View(serverInstance);
        }
        [HttpPost]
        public ActionResult EditServerInstance(ServerInstance serverInstance) {
            if (ModelState.IsValid)
            {
                try
                {
                    var selectedFarmName = Request.Form["EditServerInstanceDDL"];
                    Guid guid = Guid.Parse(selectedFarmName);
                    context.Entry(serverInstance).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index", new { index = 1 });
                }
                catch (Exception)
                {
                    return Content("编辑失败！");
                }
            }
            else {
                return View(serverInstance);
            }           
        }
        public ActionResult DetailServerInstance(Guid guid) {
            return Content("显示服务器详情。");
        }
        public ActionResult Services() {
            return View(context.Service);
        }
        [HttpPost]
        public ActionResult CreateService(ShareService.ServiceManager.Models.Service service) {
            if (ModelState.IsValid)
            {
                service.Code = Guid.NewGuid();
                service.FarmCode =Guid.Parse(Request.Form["DisplayAllFarmsName"]);
                service.UpdateDate = DateTime.Now;
                context.Entry(service).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return RedirectToAction("Index", new { index = 2 });
            }
            else {
                return Content("添加失败！");
            }
        }
        public ActionResult DeleteService(Guid guid) {
            try
            {
                var target = context.Service.Find(guid);
                if (null == target) return RedirectToAction("Index", new { index = 2 });
                context.Entry(target).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return RedirectToAction("Index", new { index = 2 });
            }
            catch (Exception)
            {
                return Content("删除失败！");
            }
        }
        public ActionResult EditService(Guid guid) {
            var service = context.Service.Find(guid);
            return View(service);
        }
        [HttpPost]
        public ActionResult EditService(ShareService.ServiceManager.Models.Service service) {
            if (ModelState.IsValid)
            {
                try
                {
                    service.FarmCode = Guid.Parse(Request.Form["FarmsNameSelect"]);
                    service.UpdateDate = DateTime.Now;
                    context.Entry(service).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index", new { index = 2 });
                }
                catch (Exception)
                {
                    return Content("编辑失败！");
                }
            }
            else {
                return View(service);
            }           
        }
        public ActionResult DetailService(Guid guid) {
            return Content("显示服务详情。");
        }       
    }
}