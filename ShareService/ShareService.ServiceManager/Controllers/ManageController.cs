using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShareService.ServiceManager.Models;
using System.Security.Claims;
using System.Net;
using System.Collections.Generic;
using ShareService.ServiceManager.DAL;

namespace ShareService.ServiceManager.Controllers
{
    [UFAuthorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;

        public ActionResult Default()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, model.UserName));
                    var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    id.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", model.UserName));
                    id.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", model.UserName));
                    var ctx = Request.GetOwinContext();
                    var authenticationManager = ctx.Authentication;
                    AuthenticationManager.SignIn(id);

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "无效的登录尝试。");
                    return View(model);
            }

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Default", "Manage");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public PartialViewResult GeneralFrameView() {
            return PartialView();
        }

        public PartialViewResult ClusterGeneralView() {
            var context = new ShareServiceContext();
            ViewBag.farmCount = context.ServerFarm.Count();
            ViewBag.serverCount = context.ServerInstance.Count();
            return PartialView();
        }

        public PartialViewResult ServiceGeneralView() {
            var context = new ShareServiceContext();
            ViewBag.serviceCount = context.Service.Count();
            ViewBag.serviceTypeCount = context.ServiceDirectory.Count();
            return PartialView();
        }

        public PartialViewResult HotGeneralView() {
            return PartialView();
        }

        public PartialViewResult ImageGeneralView() {
            var context = new ShareServiceContext();
            ViewBag.updateServices = context.Service.OrderBy(s => s.UpdateDate).Take(2).ToList();
            ViewBag.addServices = context.Service.OrderBy(s => s.CreateDate).Take(2).ToList();
            return PartialView();
        }
    }
}