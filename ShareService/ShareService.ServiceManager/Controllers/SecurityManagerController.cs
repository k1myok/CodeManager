using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShareService.ServiceManager.Controllers
{
    public class SecurityManagerController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();

        public PartialViewResult List()
        {
            return PartialView();
        }

        public PartialViewResult UserInfo()
        {
            return PartialView();
        }

        // GET: SecurityManager
        public ActionResult Index(int index=0)
        {
            return View(index);
        }
        public ActionResult Users()
        {
            var users = context.AspNetUsers.ToList();
            return View(users);
        }
        [HttpPost]

        public JsonResult CreateUser(string username,string password)
        {
            ApplicationSignInManager signinManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
         
            var user = new ApplicationUser { UserName = username, Email = username };
            var result =  userManager.Create(user, password);
            if (result.Succeeded)
            {
                signinManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                return Json(new { message="sucessed"},JsonRequestBehavior.AllowGet);
            }

            return Json(new { message="error"},JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUserByID(string id)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(id);
            var result = userManager.Delete(user);
            if (result.Succeeded) {
                return Json(new {message="successed"},JsonRequestBehavior.AllowGet);
            }
            return Json(new { message="error"},JsonRequestBehavior.AllowGet);
        }
        public JsonResult AllRoles()
        {
            return Json(context.AspNetRoles,JsonRequestBehavior.AllowGet);
        }
        public JsonResult RolesOfUser(string userId)
        {
            var roleids = context.AspNetUserRoles.Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();
            var roles = context.AspNetRoles.Where(p => roleids.Contains(p.Id)).ToList();
            return Json(roles,JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateRolesOfUser(string userId,string roleIds)
        {
            string[] postedRoles = roleIds.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var newRoleIDs = postedRoles.Select(p=>int.Parse(p)).ToList();
            var currentRolesOfUser = context.AspNetUserRoles.Where(u => u.UserId == userId).ToList();

            var toRemovedRoleIDs = currentRolesOfUser.Select(p => p.RoleId).Except(newRoleIDs);
            context.AspNetUserRoles.RemoveRange(currentRolesOfUser.Where(p => userId == p.UserId && toRemovedRoleIDs.Contains(p.RoleId)));

            var toAddedRoles = newRoleIDs.Except(currentRolesOfUser.Select(p => p.RoleId)).Select(p => new AspNetUserRoles()
            {
                UserId = userId,
                RoleId = p
            });
            context.AspNetUserRoles.AddRange(toAddedRoles);
            context.SaveChanges();
            return Json(new {message="successed"},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Roles()
        {
            var roles = context.AspNetRoles.ToList();
            return View(roles);
        }
        [HttpPost]
        public ActionResult CreateRole(AspNetRoles model)
        {
            context.AspNetRoles.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index","ServiceManage") ;
        }
        public JsonResult DeleteRoleByID(int id)
        {
            var target = context.AspNetRoles.FirstOrDefault(a => a.Id == id);
            context.AspNetRoles.Remove(target);
            try
            {
                context.SaveChanges();
                return Json(new { message = "successed" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new {message="failed"},JsonRequestBehavior.AllowGet);
            }
           
        }
        public JsonResult AllGroups()
        {
            return Json(context.Groups,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GroupsOfRole(int roleId)
        {
            var groupIds = context.RoleInGroups.Where(r => r.RoleId == roleId).Select(g => g.GroupID).ToList();
            var groups = context.Groups.Where(g => groupIds.Contains(g.Id)).ToList();
            return Json(groups,JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateGroupsOfRole(int roleId,string groupIds)
        {
            string[] postedGroups = groupIds.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var newGroupIDs = postedGroups.Select(p => int.Parse(p)).ToList();
            var currentGroupsOfRole = context.RoleInGroups.Where(u => u.RoleId == roleId).ToList();

            var toRemovedGroupIDs = currentGroupsOfRole.Select(p => p.RoleId).Except(newGroupIDs);
            context.RoleInGroups.RemoveRange(currentGroupsOfRole.Where(p => roleId == p.RoleId && toRemovedGroupIDs.Contains(p.RoleId)));

            var toAddedRoles = newGroupIDs.Except(currentGroupsOfRole.Select(p => p.RoleId)).Select(p => new RolesInGroups()
            {
                GroupID =p,
                RoleId = roleId
            });
            context.RoleInGroups.AddRange(toAddedRoles);
            context.SaveChanges();
            return Json(new {message="successed"},JsonRequestBehavior.AllowGet);
        }
        public JsonResult AllUsers()
        {
            return Json(context.AspNetUsers,JsonRequestBehavior.AllowGet);
        }
        public JsonResult UsersOfRole(int roleId)
        {
            var userIds = context.AspNetUserRoles.Where(p => p.RoleId == roleId).Select(u => u.UserId).ToList();
            var users = context.AspNetUsers.Where(u => userIds.Contains(u.Id)).ToList();
            return Json(users,JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUsersOfRole(int roleId,string userIds)
        {
            string[] postedUsers = userIds.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var newUserIDs = postedUsers.ToList();
            var currentUsersOfRole = context.AspNetUserRoles.Where(u => u.RoleId == roleId).ToList();

            var toRemovedUsers = currentUsersOfRole.Select(p => p.UserId).Except(newUserIDs);
            context.AspNetUserRoles.RemoveRange(currentUsersOfRole.Where(p => roleId == p.RoleId && toRemovedUsers.Contains(p.UserId)));

            var toAddedUsers = newUserIDs.Except(currentUsersOfRole.Select(p => p.UserId)).Select(p => new AspNetUserRoles()
            {
                UserId=p,
                RoleId = roleId
            });
            context.AspNetUserRoles.AddRange(toAddedUsers);
            context.SaveChanges();
            return Json(new { message="successed"},JsonRequestBehavior.AllowGet);
        }
        public ActionResult Groups()
        {
            var groups = context.Groups.ToList();
            return View(groups);
        }
        [HttpPost]
        public ActionResult CreateGroup(Groups model)
        {
            context.Groups.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "ServiceManage");
        }
        public JsonResult DeleteGroupById(int groupId)
        {
            var target = context.Groups.FirstOrDefault(g=>g.Id==groupId);
            context.Groups.Remove(target);
            try
            {
                context.SaveChanges();
                return Json(new { message = "successed" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { message="failed"},JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RolesOfGroup(int groupId)
        {
            var roleIds = context.RoleInGroups.Where(r => r.GroupID == groupId).Select(r => r.RoleId).ToList();
            var roles = context.AspNetRoles.Where(r => roleIds.Contains(r.Id)).ToList();
            return Json(roles,JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateRolesOfGroup(int groupId,string roleIds)
        {
            string[] postedRoles = roleIds.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var newRoleIDs = postedRoles.Select(r=>int.Parse(r)).ToList();
            var currentRolesOfGroup = context.RoleInGroups.Where(u => u.GroupID == groupId).ToList();

            var toRemovedRoles = currentRolesOfGroup.Select(p => p.RoleId).Except(newRoleIDs);
            context.RoleInGroups.RemoveRange(currentRolesOfGroup.Where(p => groupId == p.GroupID && toRemovedRoles.Contains(p.RoleId)));

            var toAddedRoles = newRoleIDs.Except(currentRolesOfGroup.Select(p => p.RoleId)).Select(p => new RolesInGroups()
            {
                GroupID = groupId,
                RoleId = p
            });
            context.RoleInGroups.AddRange(toAddedRoles);
            context.SaveChanges();
            return Json(new {message="sucessed"},JsonRequestBehavior.AllowGet);
        }
    }
}