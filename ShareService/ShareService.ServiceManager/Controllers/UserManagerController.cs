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
            if (model == null)
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
            ViewBag.ServiceCodes = serviceCodes;
            ViewBag.UserName = context.UFUser.Find(userCode).Name;
            ViewBag.UserCode = userCode;
            ViewBag.ServiceTokens = context.ServiceToken.Where(p => p.UserCode == userCode).ToList();
            //ViewBag.serviceTokens = context.ServiceToken.Where(st => st.UserCode==userCode).ToList();
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

        public JsonResult SaveEditToken(Guid serviceCode, Guid userCode, DateTime begin, DateTime end) {
            var context = new ShareServiceContext();
            var temp = context.ServiceToken.Where(st => st.UserCode == userCode && st.ServiceCode == serviceCode).FirstOrDefault();
            if (temp == null)
            {
                ServiceToken st = new ServiceToken();
                st.Code = Guid.NewGuid();
                st.ServiceCode = serviceCode;
                st.UserCode = userCode;
                st.StartDate = begin;
                st.ExpiredDate = end;
                st.SingleService = true;
                st.IsPaused = false;
                context.ServiceToken.Add(st);
                return Json(new { State = context.SaveChanges() > 0, Code = st.Code }, JsonRequestBehavior.AllowGet);
            }
            else {
                temp.StartDate = begin;
                temp.ExpiredDate = end;
                return Json(new { State = context.SaveChanges() > 0, Code = temp.Code }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteServiceToken(Guid userCode, Guid serviceCode) {
            var context = new ShareServiceContext();
            var target = context.ServiceToken.FirstOrDefault(st => st.UserCode == userCode && st.ServiceCode == serviceCode);
            //if (null != target)
            //{
            //    context.ServiceToken.Remove(target);
            //    return Json(new { State = context.SaveChanges() > 0}, JsonRequestBehavior.AllowGet);
            //}
            //else {
            //    return Json(new { State =true}, JsonRequestBehavior.AllowGet);
            //}
            context.ServiceToken.Remove(target);
            return Json(new { State = context.SaveChanges() > 0 }, JsonRequestBehavior.AllowGet);

        }

        public PartialViewResult EditGlobalTokenOfUser(Guid userCode)
        {
            var model = context.ServiceToken.FirstOrDefault(p => p.UserCode == userCode && p.ServiceCode == null);
            if (model == null)
                model = new ServiceToken() { UserCode = userCode, StartDate = DateTime.Now, ExpiredDate = DateTime.Now.AddMonths(1) };

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult EditGlobalTokenOfUser(ServiceToken tokenModel)
        {
            if (tokenModel.Code == Guid.Empty)
            {
                tokenModel.Code = Guid.NewGuid();
                context.ServiceToken.Add(tokenModel);
            }
            else
            {
                context.Entry(tokenModel).State = System.Data.Entity.EntityState.Modified;
            }
            tokenModel.ServiceCode = null;
            tokenModel.SingleService = false;
            var result = context.SaveChanges() > 0;

            return Json(new {
                State = result
            });
        }

        public JsonResult ResetGlobalTokenOfUser(Guid token)
        {
            var model = context.ServiceToken.Find(token);
            context.ServiceToken.Remove(model);
            var newToken = new ServiceToken()
            {
                Code = Guid.NewGuid(),
                ExpiredDate = model.ExpiredDate,
                StartDate = model.StartDate,
                IsPaused = model.IsPaused,
                ServiceCode = model.ServiceCode,
                SingleService = model.SingleService,
                UserCode = model.UserCode
            };
            context.ServiceToken.Add(newToken);
            var result = context.SaveChanges();
            return Json(new
            {
                State = result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}