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
    [UFAuthorize]
    public class SecurityManagerController : Controller
    {
        private ShareServiceContext context = new ShareServiceContext();
        public PartialViewResult List()
        {
            return PartialView();
        }
        public PartialViewResult UserInfo()
        {
            var isExisted = HttpContext.User.Identity != null && !string.IsNullOrEmpty(HttpContext.User.Identity.Name);
            var model = isExisted ? 
                context.UFUser.FirstOrDefault(p => p.Name == HttpContext.User.Identity.Name) : 
                new UFUser() { Name = "游客" };
            return PartialView(model);
        }
    }
}