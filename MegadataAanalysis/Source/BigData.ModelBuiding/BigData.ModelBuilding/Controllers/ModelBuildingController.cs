using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigData.ModelBuilding.DAL;
using BigData.ModelBuilding.Models;

namespace BigData.ModelBuilding.Controllers
{
    public class ModelBuildingController : Controller
    {
        private ModelBuildingContext context = new ModelBuildingContext();

        public PartialViewResult List()
        {
            return PartialView(context.BuildingModel);
        }
        public PartialViewResult BMList()
        {
            return PartialView(context.BuildingModel);
        }
        public ActionResult DirectoryTree()
        {
            var directories = context.BuildingModelDirectory.OrderBy(p => p.Parent).ToList();
            var rootItems = directories.Where(p => p.Parent == null).ToList();
            var childsItems = directories.Where(p => p.Parent != null).ToList();

            this.Insert(rootItems, childsItems);
            return PartialView(rootItems);
        }

        public ActionResult DirectoryTreeItem(BuildingModelDirectory item)
        {
            return PartialView(item);
        }

        private void Insert(List<BuildingModelDirectory> rootItems, List<BuildingModelDirectory> childsItems)
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

        private bool Insert(List<BuildingModelDirectory> source, BuildingModelDirectory function)
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
                        item.Children = new List<BuildingModelDirectory>();
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
                BuildingModelDirectory newNode = new BuildingModelDirectory();
                newNode.Code = Guid.NewGuid();
                newNode.Name = child;
                newNode.Parent = parent;
                context.BuildingModelDirectory.Add(newNode);
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
                var target = context.BuildingModelDirectory.First(rd => rd.Code == code);
                context.BuildingModelDirectory.Remove(target);
                if (!string.IsNullOrEmpty(childCodes))
                {
                    var childsCodeToDelete = childCodes.Split(',').Select(c => c.Trim()).Select(g => Guid.Parse(g));
                    var targetChilds = context.BuildingModelDirectory.Where(rd => childsCodeToDelete.Contains(rd.Code));
                    context.BuildingModelDirectory.RemoveRange(targetChilds);
                }
                var result = context.SaveChanges() > 0;
                return Json(new { State = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { State = false }, JsonRequestBehavior.AllowGet);
            }
        }

        //zwx
        public PartialViewResult BuildingModels(Guid code)
        {
            var model = context.BuildingModel.Where(p => p.DirectoryCode == code).ToList();
            return PartialView(model);
        }

        public PartialViewResult ModelDetail(Guid code)
        {
            var moel = context.BuildingModel.FirstOrDefault(p => p.Code == code);
            return PartialView(moel);
        }

        public PartialViewResult BasicInfoOfModel(Guid code)
        {
            var moel = context.BuildingModel.FirstOrDefault(p => p.Code == code);
            return PartialView(moel);
        }

        public PartialViewResult FieldsInfoOfModel(Guid code)
        {
            //var model = from a in context.BuildingModelFieldsInfo
            //            join b in context.BaseField
            //            on a.FieldCode equals b.Code
            //            where a.ModelCode == code
            //            select b;
            var model = context.BaseField;

            //var model = context.AnalysisModelFieldsInfo.Join<AnalysisModelFieldsInfo, BaseField, Guid, BaseField>
            //    (context.BaseField, p => p.FieldCode, a => a.Code, (c, d) => d).ToList();
            return PartialView(model);
        }

        public PartialViewResult CreateModifyModelView(Guid code)
        {

            var model = context.BuildingModel.FirstOrDefault(p => p.Code == code);
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

        public PartialViewResult ListDetail(Guid code)
        {
            var result = context.BuildingModel.FirstOrDefault(p => p.Code == code);
            ViewBag.SystemModelSource = context.AnalysisModel.Select(p => new SelectListItem() { Text = p.Name, Value = p.Code.ToString() });
            ViewBag.SourceType = new List<SelectListItem>() { 
                new SelectListItem(){ Text = "表", Value = "0"},
                new SelectListItem(){ Text = "视图", Value = "1"}
            };
            var sql = "";
            if (result.SourceType == 1)
                 sql = "select Name,'1'as SourceType from sys.all_views WHERE SCHEMA_ID='1'";
            else
                sql = "select Name,'0' as SourceType from sys.all_objects where type='U'";

            ViewBag.SourceNameList = context.Database.SqlQuery<Table>(sql).Select(p => new SelectListItem() {
                Text = p.Name,
                 Value = p.Name
            });
            ViewBag.OutputTypeList = new List<SelectListItem>() { 
                new SelectListItem(){ Text = "表", Value = "0"},
                new SelectListItem(){ Text = "视图", Value = "1"}
            };
            return PartialView(result);
        }

        public PartialViewResult GetFieldsInfo(Guid code)
        {
            var result = context.AnalysisModel.Where(p => p.Code == code).Select(am => new SelectListItem { Value = am.Name, Text = am.Name }).ToList();
            ViewBag.result = result;
            return PartialView();
        }

        public PartialViewResult CreateBasicInfoModel()
        {
            var result = context.Database.SqlQuery<Table>("select Name,'0'as SourceType from sys.all_objects where type='U'").ToList();
            ViewBag.resul = result.Select(am => new SelectListItem { Value = am.Name, Text = am.Name }).ToList();
            List<SelectListItem> SourceType = new List<SelectListItem>{
            new SelectListItem{Text="表",Value="0"},
            new SelectListItem{Text="视图",Value="1"}
            };
            List<SelectListItem> OutputType = new List<SelectListItem>{
            new SelectListItem{Text="表",Value="0"},
            new SelectListItem{Text="视图",Value="1"}
            };
            ViewBag.SourceType = new SelectList(SourceType, "Value", "Text");
            ViewBag.OutputType = new SelectList(OutputType, "Value", "Text");
            return PartialView(new BuildingModel());
        }
        [HttpPost]

        public JsonResult CreateBasicInfoModel(BuildingModel model)
        {
            model.CreateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.UpdateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.Code = Guid.NewGuid();
            context.BuildingModel.Add(model);
            var result = context.SaveChanges() > 0;
            return Json(new { Status = result });
        }
        [HttpPost]

        public JsonResult EditBasicInfoModel(BuildingModel model)
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
        public PartialViewResult GetAnalysisModel()
        {
            var result = context.AnalysisModel.Select(am => new SelectListItem { Value = am.Code.ToString(), Text = am.Name }).ToList();
            ViewBag.result = result;
            return PartialView();
        }
        public JsonResult DeleteBasicInfoModel(Guid code)
        {
            var model = context.BuildingModel.Find(code);
            if (model != null)
            {
                context.BuildingModel.Remove(model);
                context.SaveChanges();
                return Json(new
                {
                    State = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                State = false
            }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult FilesInfo(Guid code)
        {
            var baseFields = (from a in context.AnalysisModel
                              join b in context.AnalysisModelFieldsInfo
                              on a.Code equals b.ModelCode
                              join c in context.BaseField
                              on b.FieldCode equals c.Code
                              where a.Code == code
                              select c).ToList();
            return PartialView(baseFields);
        }
        public PartialViewResult GetTableName(string sourceType,Guid code)
        {
                   
            var sql = "";
            var SName=context.BuildingModel.Where(p=>p.Code==code).Select(p=>p.SourceName).ToList();
            var SourceName = "";
            if (SName.Count() > 0)
            {
                SourceName = SName[0].ToString();
            }
            switch (sourceType)
            {
                case "0":
                case null:
                    sql = "select Name,'0' as SourceType from sys.all_objects where type='U'";
                    break;
                case "1":
                    sql = "select Name,'1'as SourceType from sys.all_views WHERE SCHEMA_ID='1'";
                    break;
            }
            var result = context.Database.SqlQuery<Table>(sql).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in result)
            {
               if (item.Name == SourceName)
                    list.Add(new SelectListItem { Text = item.Name, Value = item.Name, Selected = true });
              else
                    list.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }
            ViewBag.resul = list;
            return PartialView();
        }


        public PartialViewResult GetFieldInformation(string SourceName)
        {
         
            var sql = "SELECT TABLE_CATALOG AS[Database],TABLE_NAME AS TableName, COLUMN_NAME AS ColumnName,DATA_TYPE AS DataType FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME ="+"'"+SourceName+"'"; 
            var result = context.Database.SqlQuery<Table>(sql).ToList();
            ViewBag.result = result.Select(am => new SelectListItem { Value = am.ColumnName +"  "+ "(" + am.DataType + ")", Text = am.ColumnName + "  " + "(" +am.DataType+")" }).ToList();
            return PartialView();
        }
    }         
    }