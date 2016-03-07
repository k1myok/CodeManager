using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareService.ServiceManager.DAL;
using ShareService.ServiceManager.Models;

namespace ShareService.ServiceManager.Controllers
{
    public class MetadataManagerController : Controller
    {
        public ActionResult Create()
        {
            var types = new List<string>() {
                "字符串",
                "数值"
            };

            ViewBag.FieldTypes = types.Select(p => new SelectListItem() { Text = p, Value = p });

            var model = new BaseMetadata() {};
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Create(BaseMetadata model)
        {
            using (var context = new ShareServiceContext())
            {               
                model.Code = Guid.NewGuid();
                context.BaseMetadata.Add(model);
                if (context.SaveChanges() > 0)
                    return Json(new { State = true  });
                else
                    return Json(new { State = false });
            }
        }
    }
}