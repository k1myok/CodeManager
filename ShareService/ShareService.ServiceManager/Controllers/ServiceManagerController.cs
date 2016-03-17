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
    public class ServiceManagerController : Controller
    {
        // GET: GISResourceManager
        private ShareServiceContext context = new ShareServiceContext();

        public ActionResult List(Guid directoryCode)
        {
            ViewBag.DirectoryCode = directoryCode;
            return PartialView(context.Service.Where(p => p.Directory == directoryCode));
        }

        public ActionResult Create(Guid directoryCode)
        {
            ViewBag.Farms = context.ServerFarm.Select(p => new SelectListItem() { Text = p.Name, Value = p.Code.ToString() });
            var model = new ShareService.ServiceManager.Models.Service()
            {
                Directory = directoryCode
            };
            this.AttachMetadataDetails(model);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(ShareService.ServiceManager.Models.Service model)
        {
            var currentDate = DateTime.Now;
            model.Code = Guid.NewGuid();
            model.CreateDate = currentDate;
            model.UpdateDate = currentDate;
            this.UpdateResourceMetadataToDB(model);
            context.Service.Add(model);
            var reuslt = context.SaveChanges() > 0;
            return Json(new
            {
                State = reuslt
            });
        }

        public ActionResult Edit(Guid resourceCode)
        {
            ViewBag.Farms = context.ServerFarm.Select(p => new SelectListItem() { Text = p.Name, Value = p.Code.ToString() });
            var model = context.Service.FirstOrDefault(p => p.Code == resourceCode);
            this.AttachMetadataDetails(model);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Edit(ShareService.ServiceManager.Models.Service model)
        {
            this.UpdateResourceMetadataToDB(model);
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            var reuslt = context.SaveChanges() > 0;
            return Json(new
            {
                State = true
            });
        }

        public ActionResult Detail(Guid resourceCode)
        {
            ViewBag.Farms = context.ServerFarm.Select(p => new SelectListItem() { Text = p.Name, Value = p.Code.ToString() });
            var model = context.Service.FirstOrDefault(p => p.Code == resourceCode);
            this.AttachMetadataDetails(model);
            return PartialView(model);
        }

        public JsonResult Delete(Guid resourceCode)
        {
            var target = context.Service.Find(resourceCode);
            if(target != null)
            {
                context.Service.Remove(target);
                var result = context.SaveChanges();
                return Json(new
                {
                    State = result
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                State = false
            }, JsonRequestBehavior.AllowGet);
        }

        private void AttachMetadataDetails(ShareService.ServiceManager.Models.Service model)
        {
            var metadataList = context.BaseMetadata.ToList();
            var metadataDetails = metadataList.Select(metadata => {
                var targetResourceMetadata = context.ServiceMetadata.FirstOrDefault(p => p.MetadataCode == metadata.Code && p.ServiceCode == model.Code);
                if (targetResourceMetadata == null)
                {
                    targetResourceMetadata = new ServiceMetadata()
                    {
                        MetadataCode = metadata.Code
                    };
                }
                targetResourceMetadata.BaseMetadata = metadata;

                return targetResourceMetadata;
            });
            model.MetadataDetails = metadataDetails.ToList();
        }

        private void UpdateResourceMetadataToDB(ShareService.ServiceManager.Models.Service model)
        {
            if (model.MetadataDetails != null)
            {
                model.MetadataDetails.ForEach(p => {
                    if (p.Code != Guid.Empty)
                        context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    else
                    {
                        p.Code = Guid.NewGuid();
                        p.ServiceCode = model.Code;
                        context.ServiceMetadata.Add(p);
                    }
                });
            }
        }
    }
}