using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShareService.Service.ARR;
using ShareService.ServiceManager.Controllers;
using ShareService.ServiceManager.DAL;

/// <summary>
/// UrlReWriter 的摘要说明
/// </summary>
public class WebAuthenticate : IHttpModule
{
    public const string LoginResultKey = "IsLogined";
    public const string LoginTokenKey = "LoginToken";

    public void Init(HttpApplication context)
    {
        //context.BeginRequest += context_BeginRequest;
        //context.AuthenticateRequest += context_AuthenticateRequest;
        //context.AuthorizeRequest += Context_AuthorizeRequest;
    }

    private void Context_AuthorizeRequest(object sender, EventArgs e)
    {
        var application = (HttpApplication)sender;
        if (application.User == null || application.User.Identity == null || string.IsNullOrEmpty(application.User.Identity.Name))
        {
            var context = application.Context;
            string token = context.Request.QueryString.Get(LoginTokenKey);
            if (string.IsNullOrEmpty(token))
            {
                HttpContext.Current.Response.Redirect("http://localhost:802/Account/Login");
            }
            else
            {
                var result = Login(token);

            }
        }

        //if (CheckValid(application.Context))
        //    return;
        //else
        //    FormsAuthentication.RedirectToLoginPage();
    }

    bool CheckValid(HttpContext context)
    {
        return true;
    }

    void context_BeginRequest(object sender, EventArgs e)
    {
        var application = (HttpApplication)sender;

        //this.Login(application);
    }

    void context_AuthenticateRequest(object sender, EventArgs e)
    {
        var application = (HttpApplication)sender;
        //if (application.Request.Path.StartsWith("/Public"))
        //    return;
        //this.CheckIsLogined(application);
    }

    bool Login(string token)
    {
        var tokenModel = ARRToken.Parse(token);
        //var control = new AccountController();
        var user = new ShareServiceContext().UFUser.Find(Guid.Parse(tokenModel.UserCode));
        var callBackURL = "http://localhost:802/arcgis/rest/services/Finance";
        var a = System.Web.HttpUtility.UrlEncode(callBackURL);
        HttpContext.Current.Response.Redirect("http://localhost:802/Account/Login1?userName=" + user.Name + "&passwords=" + user.Password + "&returnUrl=" + a);
        return true;
        //return control.Login(user.Name, user.Password) == Microsoft.AspNet.Identity.Owin.SignInStatus.Success;
    }

    void CheckIsLogined(HttpApplication application)
    {
        var context = application.Context;
        if(context.Request.IsAuthenticated)
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(WebAuthenticate.LoginResultKey, "true"));
        else
        {
            var cookie = context.Request.Cookies.Get(LoginResultKey);
            if (cookie == null || cookie.Value != "true")
            {
                HttpResponse response = context.Response;
                FormsAuthentication.RedirectToLoginPage();
                //response.Redirect("Login1.aspx");
            }
        }
    }

    public void Dispose()
    {
    }
}


public class UFAuthorizeAttribute : AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        //return true;
        var result = httpContext.User.Identity != null && !string.IsNullOrEmpty(httpContext.User.Identity.Name);
        return result;
        return base.AuthorizeCore(httpContext);
    }
}

public class CustomerIdentity : IIdentity
{
    public CustomerIdentity(string name)
    {
        this.Name = name;
    }
    public string AuthenticationType
    {
        get
        {
            return "Customer";
        }
    }

    public bool IsAuthenticated
    {
        get
        {
            return true;
        }
    }

    public string Name { get; private set; }
}