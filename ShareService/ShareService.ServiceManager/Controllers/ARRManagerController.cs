using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareService.Service.ARR;
using ShareService.ServiceManager.DAL;

namespace ShareService.ServiceManager.Controllers
{
    [UFAuthorize]
    public class ARRManagerController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();
        private ARRFarmManager arrFarmManager = new ARRFarmManager();
        // GET: ARRManager
        public JsonResult ResetServices()
        {
            var services = context.Service.ToList();
            var rules = services.Select(p => {
                var rule = new ARRRule() {
                     InURL = p.URL,
                     Name = p.Code.ToString(),
                     ToFarm = p.FarmCode.ToString()
                };
                return rule;
            });
            var result = arrFarmManager.ResetRules(rules.ToArray());
            if (result)
                result = arrFarmManager.Save();
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResetFarms()
        {
            var farms = context.ServerFarm.ToList();
            var servers = context.ServerInstance.ToList();

            var arrFarms = farms.Select(p => {
                var farm = new ARRFarm() { Name = p.Code.ToString() };
                farm.Servers = servers.Where(pp => pp.FarmCode == p.Code).Select(server => new ARRServer(server.Address, server.HttpPort)).ToList();
                return farm;
            });
            var result = arrFarmManager.ResetFarms(arrFarms.ToArray());
            if(result)
                result = arrFarmManager.Save();
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
    }
}