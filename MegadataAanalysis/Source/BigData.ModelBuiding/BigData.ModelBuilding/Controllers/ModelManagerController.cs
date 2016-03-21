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
            var model = context.AnalysisModel.Where(p => p.DirectoryCode == code).ToList();
            return PartialView(model);
         }


        public PartialViewResult AnalysisModelsInformation(Guid code)
        {
            var bsaicInformation = context.AnalysisModel.Where(p => p.Code == code );
            return PartialView(bsaicInformation);
        }

        public PartialViewResult ModelDetail(Guid code)
        {
            var moel = context.AnalysisModel.FirstOrDefault(p => p.Code == code );
            return PartialView(moel);
        }

        public PartialViewResult BasicInfoOfModel(Guid code)
        {
            var moel = context.AnalysisModel.FirstOrDefault(p => p.Code == code );
            return PartialView(moel);
        }

        public PartialViewResult FieldsInfoOfModel(Guid code)
        {
            var models = from a in context.AnalysisModel
                         join b in context.AnalysisModelFieldsInfo
                         on a.Code equals b.ModelCode
                         join c in context.BaseField
                         on b.FieldCode equals c.Code
                         select c;
                         
            return PartialView(models);
        }
    }
}