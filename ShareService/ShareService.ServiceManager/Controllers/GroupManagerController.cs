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
    public class GroupManagerController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();

        // GET: UserManager
        public PartialViewResult List()
        {
            return PartialView(context.UFGroup);
        }

        public PartialViewResult Edit(Guid code)
        {
            var model = context.UFGroup.Find(code);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Edit(UFGroup model)
        {
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            var result = context.SaveChanges() > 0;
            return Json(new { State = result });
        }

        public PartialViewResult Detail(Guid code)
        {
            var model = context.UFGroup.Find(code);
            return PartialView(model);
        }

        public ActionResult DirectoryTree()
        {
            var directories = context.UFGroup.OrderBy(p => p.Parent).ToList();
            var rootItems = directories.Where(p => p.Parent == null).ToList();
            var childsItems = directories.Where(p => p.Parent != null).ToList();

            this.Insert(rootItems, childsItems);
            return PartialView(rootItems);
        }

        public ActionResult DirectoryTreeItem(UFGroup resourceItem)
        {
            return PartialView(resourceItem);
        }

        private void Insert(List<UFGroup> rootItems, List<UFGroup> childsItems)
        {
            var count = childsItems.Count;
            for (int index = 0; index < childsItems.Count; index++)
            {
                var layer = childsItems[index];
                if (Insert(rootItems, layer))
                {
                    childsItems.RemoveAt(index);
                    index--;
                }
            }
            if (childsItems.Count != count)
                Insert(rootItems, childsItems);
        }

        private bool Insert(List<UFGroup> source, UFGroup function)
        {
            if (source == null)
                return false;
            var result = false;
            for (int index = 0; index < source.Count; index++)
            {
                var item = source[index];
                if (function.Parent == item.Code)
                {
                    if (item.Children == null)
                        item.Children = new List<UFGroup>();
                    item.Children.Add(function);
                    return true;
                }
                else
                {
                    result = Insert(item.Children, function);
                    if (result)
                        return true;
                }
            }

            return result;
        }

        public JsonResult AddGroup(Guid? parent, string child)
        {
            try
            {
                UFGroup newNode = new UFGroup();
                newNode.Code = Guid.NewGuid();
                newNode.Name = child;
                newNode.Parent = parent;
                context.UFGroup.Add(newNode);
                var result = context.SaveChanges() > 0;
                return Json(new { State = result, Code = newNode.Code }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteGroup(Guid code, string childCodes)
        {
            try
            {
                var target = context.UFGroup.First(rd => rd.Code == code);
                context.UFGroup.Remove(target);
                if (!string.IsNullOrEmpty(childCodes))
                {
                    var childsCodeToDelete = childCodes.Split(',').Select(c => c.Trim()).Select(g => Guid.Parse(g));
                    var targetChilds = context.UFGroup.Where(rd => childsCodeToDelete.Contains(rd.Code));
                    context.UFGroup.RemoveRange(targetChilds);
                }
                var result = context.SaveChanges() > 0;
                return Json(new { State = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public PartialViewResult UsersOfGroup(Guid code)
        {
            var userCodes = from g in context.UFUserInGroup
                         join u in context.UFUser
                         on g.UserCode equals u.Code
                         where g.GroupCode == code
                         select u.Code;
            var models = from u in context.UFUser select u;
            ViewBag.groupCode = code;
            ViewBag.userCodes = userCodes.ToList();
            return PartialView(models);                        
        }

        public JsonResult AddUserToGroup(string userCode, string groupCode)
        {
            var userGuid = Guid.Parse(userCode);
            var groupGuid = Guid.Parse(groupCode);
            var target = context.UFUserInGroup.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFUserInGroup, bool>>)(u => u.GroupCode == groupGuid && u.UserCode == userGuid));
            if (target != null)
            {
                return Json(new { State = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                var ufuseringroup = new UFUserInGroup()
                {
                    GroupCode = groupGuid,
                    UserCode = userGuid
                };

                context.UFUserInGroup.Add(ufuseringroup);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public JsonResult RemoveUserFromGroup(string userCode, string groupCode)
        {
            var userGuid = Guid.Parse(userCode);
            var groupGuid = Guid.Parse(groupCode);
            var target = context.UFUserInGroup.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFUserInGroup, bool>>)(u => u.GroupCode == groupGuid && u.UserCode == userGuid));
            if (target == null)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
            else {
                context.UFUserInGroup.Remove(target);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public PartialViewResult RolesOfGroup(Guid code)
        {
            var roleCodes = from g in context.UFGroupInRole
                            join u in context.UFRole
                            on g.RoleCode equals u.Code
                            where g.GroupCode == code
                            select u.Code;
            var models = from u in context.UFRole select u;
            ViewBag.groupCode = code;
            ViewBag.roleCodes = roleCodes.ToList();
            return PartialView(models);
        }


        public JsonResult AddRoleToGroup(string roleCode, string groupCode)
        {
            var roleGuid = Guid.Parse(roleCode);
            var groupGuid = Guid.Parse(groupCode);
            var target = context.UFGroupInRole.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFGroupInRole, bool>>)(u => u.GroupCode == groupGuid && u.RoleCode == roleGuid));
            if (target != null)
            {
                return Json(new { State = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                var ufgroupinrole = new UFGroupInRole()
                {
                    GroupCode = groupGuid,
                    RoleCode = roleGuid
                };

                context.UFGroupInRole.Add(ufgroupinrole);
                return Json(
                    new
                    {
                        State = context.SaveChanges() > 0
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public JsonResult RemoveRoleFromGroup(string roleCode, string groupCode)
        {
            var roleGuid = Guid.Parse(roleCode);
            var groupGuid = Guid.Parse(groupCode);
            var target = context.UFGroupInRole.FirstOrDefault((System.Linq.Expressions.Expression<Func<UFGroupInRole, bool>>)(u => u.GroupCode == groupGuid && u.RoleCode == roleGuid));
            if (target == null)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
            else {
                context.UFGroupInRole.Remove(target);
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