using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;

namespace ResoureManager.Content.Controllers
{
    public class DirectoryManagerController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();

        // GET: DirectoryManager
        public ActionResult List()
        {
            return PartialView();
        }

        public ActionResult DirectoryTree()
        {
            var directories = context.ServiceDirectory.OrderBy(p => p.Parent).ToList();
            var rootItems = directories.Where(p => p.Parent == null).ToList();
            var childsItems = directories.Where(p => p.Parent != null).ToList();

            this.Insert(rootItems, childsItems);
            return PartialView(rootItems);
        }

        public ActionResult DirectoryTreeItem(ServiceDirectory resourceItem)
        {
            return PartialView(resourceItem);
        }
        private void Insert(List<ServiceDirectory> rootItems, List<ServiceDirectory> childsItems)
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

        private bool Insert(List<ServiceDirectory> source, ServiceDirectory function)
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
                        item.Children = new List<ServiceDirectory>();
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

        public JsonResult AddDirectoryTreeNode(Guid? parent,string child) {
            try
            {
                var parentNode = context.ServiceDirectory.FirstOrDefault(rd => rd.Code == parent);
                ServiceDirectory newNode = new ServiceDirectory();
                newNode.Code = Guid.NewGuid();
                newNode.Name = child;
                if (parentNode != null) {
                    newNode.Parent = parentNode.Code;
                }
                context.ServiceDirectory.Add(newNode);
                var result =  context.SaveChanges() > 0;
                return Json(new { State = result, Code = newNode.Code}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteDirectoryTreeNode(Guid code,string childCodes) {
            try
            {
                var target = context.ServiceDirectory.First(rd => rd.Code == code);
                context.ServiceDirectory.Remove(target);
                if (!string.IsNullOrEmpty(childCodes))
                {
                    var childsCodeToDelete = childCodes.Split(',').Select(c => c.Trim()).Select(g => Guid.Parse(g));
                    var targetChilds = context.ServiceDirectory.Where(rd => childsCodeToDelete.Contains(rd.Code));
                    context.ServiceDirectory.RemoveRange(targetChilds); 
                }           
                var result= context.SaveChanges()>0;
                return Json(new {State=result},JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { State =false},JsonRequestBehavior.AllowGet);
            }
        }
    }
}