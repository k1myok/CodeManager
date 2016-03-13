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
        public PartialViewResult List()
        {
            return PartialView();
        }
        public PartialViewResult UserInfo()
        {
            return PartialView();
        }
    }
}