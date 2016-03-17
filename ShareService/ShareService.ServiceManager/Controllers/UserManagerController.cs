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

        public PartialViewResult EditRolesOfUser(Guid userCode) {
            var roleCodes = context.UFUserInRole.Where(ur => ur.UserCode == userCode).Select(e => e.RoleCode).ToList();
            //var rolesOfUser = context.UFRole.Where(r => roleCodes.Contains(r.Code)).ToList();
            var allRoles = context.UFRole.ToList();
            ViewBag.roleCodes = roleCodes;
            ViewBag.userCode = userCode;
            return PartialView(allRoles);
        }

        public JsonResult AddRoleToUser(string userCode, string roleCode)
        {
            var userGuid = Guid.Parse(userCode);
            var roleGuid = Guid.Parse(roleCode);
            var target = context.UFUserInRole.FirstOrDefault(u => u.RoleCode == roleGuid && u.UserCode == userGuid);
            if (target != null)
            {
                return Json(new { State = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                var userInRole = new UFUserInRole() {
                    RoleCode = roleGuid,
                    UserCode = userGuid
                };

                context.UFUserInRole.Add(userInRole);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    }, 
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public JsonResult RemoveRoleFromUser(string userCode, string roleCode)
        {
            var userGuid = Guid.Parse(userCode);
            var roleGuid = Guid.Parse(roleCode);
            var target = context.UFUserInRole.FirstOrDefault(u => u.RoleCode == roleGuid && u.UserCode == userGuid);
            if (target == null)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
            else {
                context.UFUserInRole.Remove(target);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    }, 
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public PartialViewResult EditServicesOfUser(Guid userCode) {
            var serviceCodes = context.UFServicesOfUser.Where(su => su.UserCode == userCode).Select(e => e.ServiceCode).ToList();
            var allServices = context.Service.ToList();
            ViewBag.serviceCodes = serviceCodes;
            ViewBag.userCode = userCode;
            return PartialView(allServices);
        }

        public JsonResult AddServiceToUser(string userCode, string serviceCode)
        {
            var userGuid = Guid.Parse(userCode);
            var serviceGuid = Guid.Parse(serviceCode);
            var target = context.UFServicesOfUser.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFServicesOfUser, bool>>)(u => u.ServiceCode == serviceGuid && u.UserCode == userGuid));
            if (target != null)
            {
                return Json(new { State = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                var ufserviceofuser = new UFServicesOfUser()
                {
                    ServiceCode = serviceGuid,
                    UserCode = userGuid
                };

                context.UFServicesOfUser.Add(ufserviceofuser);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public JsonResult RemoveServiceFromUser(string userCode, string serviceCode)
        {
            var userGuid = Guid.Parse(userCode);
            var serviceGuid = Guid.Parse(serviceCode);
            var target = context.UFServicesOfUser.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFServicesOfUser, bool>>)(u => u.ServiceCode == serviceGuid && u.UserCode == userGuid));
            if (target == null)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
            else {
                context.UFServicesOfUser.Remove(target);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
        }
    }
}