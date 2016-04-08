using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigData.ModelBuilding.DAL;
using BigData.ModelBuilding.Models;

namespace BigData.ModelBuilding.Controllers
{
    public class ModelManagerController : Controller
    {
        private ModelBuildingContext context = new ModelBuildingContext();

        public ActionResult DirectoryTree()
        {
            var directories = context.AnalysisModelDirectory.OrderBy(p => p.Parent).ToList();
            var rootItems = directories.Where(p => p.Parent == null).ToList();
            var childsItems = directories.Where(p => p.Parent != null).ToList();

            this.Insert(rootItems, childsItems);
            return PartialView(rootItems);
        }

        public ActionResult DirectoryTreeItem(AnalysisModelDirectory item)
        {
            return PartialView(item);
        }

        private void Insert(List<AnalysisModelDirectory> rootItems, List<AnalysisModelDirectory> childsItems)
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

        private bool Insert(List<AnalysisModelDirectory> source, AnalysisModelDirectory function)
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
                        item.Children = new List<AnalysisModelDirectory>();
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

        public JsonResult AddTreeNode(Guid? parent, string child)
        {
            try
            {
                AnalysisModelDirectory newNode = new AnalysisModelDirectory();
                newNode.Code = Guid.NewGuid();
                newNode.Name = child;
                newNode.Parent = parent;
                context.AnalysisModelDirectory.Add(newNode);
                var result = context.SaveChanges() > 0;
                return Json(new { State = result, Code = newNode.Code }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteTreeNode(Guid code, string childCodes)
        {
            try
            {
                var target = context.AnalysisModelDirectory.First(rd => rd.Code == code);
                context.AnalysisModelDirectory.Remove(target);
                if (!string.IsNullOrEmpty(childCodes))
                {
                    var childsCodeToDelete = childCodes.Split(',').Select(c => c.Trim()).Select(g => Guid.Parse(g));
                    var targetChilds = context.AnalysisModelDirectory.Where(rd => childsCodeToDelete.Contains(rd.Code));
                    context.AnalysisModelDirectory.RemoveRange(targetChilds);
                }
                var result = context.SaveChanges() > 0;
                return Json(new { State = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
        }

        //jzmhang code
        public PartialViewResult AnalysisModels(Guid code)
        {
            ViewBag.DirectoryCode = code;
            var model = context.AnalysisModel.Where(p => p.DirectoryCode == code).ToList();
            return PartialView(model);
        }

        public PartialViewResult ModelDetail(Guid code)
        {
            var moel = context.AnalysisModel.FirstOrDefault(p => p.Code == code);
            return PartialView(moel);
        }

       public PartialViewResult CreateAddModelView(Guid directoryCode)
        {
            var model = new AnalysisModel();
            model.DirectoryCode = directoryCode;
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult CreateAddModelView(AnalysisModel model)
        {
            model.Code = Guid.NewGuid();
            context.AnalysisModel.Add(model);
            var result = context.SaveChanges() > 0;
            return Json(new
            {
                State = result
            });
        }

        public PartialViewResult CreateModifyModelView(Guid code)
        {
           
            var model = context.AnalysisModel.FirstOrDefault(p => p.Code == code);
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult CreateModifyModelView(AnalysisModel model)
        {
            if (ModelState.IsValid)
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

        public JsonResult DeleteModelDetail(Guid code)
        {
            var model = context.AnalysisModel.FirstOrDefault(p => p.Code == code);
            context.AnalysisModel.Remove(model);
            var result = context.SaveChanges() > 0;
            return Json(new {
                State = result
            }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult FieldsInfoOfModel(Guid code)
        {
            var model = from a in context.AnalysisModelFieldsInfo
                       join b in context.BaseField
                       on a.FieldCode equals b.Code
                        where a.ModelCode == code
                       select b;
            return PartialView(model);
        }


        public PartialViewResult BasicInfoOfModel(Guid code)
        {
            var moel = context.AnalysisModel.FirstOrDefault(p => p.Code == code);
            return PartialView(moel);
        }

    }
}