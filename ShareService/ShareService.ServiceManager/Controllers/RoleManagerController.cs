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

        public PartialViewResult EditUsersOfRole(Guid code) {
            var userCodes = context.UFUserInRole.Where(ur => ur.RoleCode == code).Select(u=>u.UserCode).ToList();
            var allUsers = context.UFUser.ToList();
            ViewBag.userCodes = userCodes;
            ViewBag.roleCode = code;
            return PartialView(allUsers);
        }

        //[HttpPost]
        //public JsonResult EditUsersOfRole( string userCode,string roleCode) {
        //    Guid userGuid = Guid.Parse(userCode);
        //    var target = context.UFUserInRole.FirstOrDefault(ur => ur.UserCode == userGuid);
        //    if (target != null)
        //    {
        //        context.UFUserInRole.Remove(target);
        //    }
        //    else {
        //        context.UFUserInRole.Add(new UFUserInRole() { RoleCode = Guid.Parse(roleCode), UserCode = Guid.Parse(userCode) });
        //    }
        //    return Json(new { State = context.SaveChanges() > 0 });
        //}

        public JsonResult AddUserToRole(string userCode, string roleCode)
        {
            var userGuid = Guid.Parse(userCode);
            var roleGuid = Guid.Parse(roleCode);
            var target = context.UFUserInRole.FirstOrDefault(u => u.RoleCode == roleGuid && u.UserCode == userGuid);
            if (target != null)
            {
                return Json(new { State = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                var userInRole = new UFUserInRole()
                {
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

        public JsonResult RemoveUserFromRole(string userCode, string roleCode)
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

        public PartialViewResult EditServicesOfRole(Guid roleCode)
        {
            var serviceCodes = context.UFServicesOfRole.Where(su => su.RoleCode == roleCode).Select(e => e.ServiceCode).ToList();
            var allServices = context.Service.ToList();
            ViewBag.serviceCodes = serviceCodes;
            ViewBag.roleCode = roleCode;
            return PartialView(allServices);
        }

        public JsonResult AddServiceToRole(string roleCode, string serviceCode)
        {
            var roleGuid = Guid.Parse(roleCode);
            var serviceGuid = Guid.Parse(serviceCode);
            var target = context.UFServicesOfRole.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFServicesOfRole, bool>>)(u => u.ServiceCode == serviceGuid && u.RoleCode == roleGuid));
            if (target != null)
            {
                return Json(new { State = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                var ufserviceofrole = new UFServicesOfRole()
                {
                    ServiceCode = serviceGuid,
                    RoleCode = roleGuid
                };

                context.UFServicesOfRole.Add(ufserviceofrole);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public JsonResult RemoveServiceFromRole(string roleCode, string serviceCode)
        {
            var roleGuid = Guid.Parse(roleCode);
            var serviceGuid = Guid.Parse(serviceCode);
            var target = context.UFServicesOfRole.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFServicesOfRole, bool>>)(u => u.ServiceCode == serviceGuid && u.RoleCode == roleGuid));
            if (target == null)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
            else {
                context.UFServicesOfRole.Remove(target);
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