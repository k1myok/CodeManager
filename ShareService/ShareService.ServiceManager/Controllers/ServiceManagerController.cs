using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;

namespace ShareService.ServiceManager.Controllers
{
    public class ServiceManagerController : Controller
    {
        // GET: GISResourceManager
        private ShareServiceContext context = new ShareServiceContext();

        public ActionResult Create(Guid directoryCode)
        {
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
            model.Code = Guid.NewGuid();
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
            var model = context.Service.FirstOrDefault(p => p.Code == resourceCode);
            this.AttachMetadataDetails(model);
            return PartialView(model);
        }

        public JsonResult Delete(Guid resourceCode)
        {
            var reuslt = context.Database.ExecuteSqlCommand(string.Format("delete from [Service] where Code='{0}'", resourceCode));
            return Json(new
            {
                State = reuslt > 0
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