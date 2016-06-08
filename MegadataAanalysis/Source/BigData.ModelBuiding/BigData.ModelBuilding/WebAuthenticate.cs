using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;


/// <summary>
/// UrlReWriter 的摘要说明
/// </summary>
public class WebAuthenticate : IHttpModule
{
    public const string LoginResultKey = "IsLogined";
    public const string LoginTokenKey = "LoginToken";

    public void Init(HttpApplication context)
    {
        context.BeginRequest += context_BeginRequest;
        context.AuthenticateRequest += context_AuthenticateRequest;
    }

    void context_BeginRequest(object sender, EventArgs e)
    {
        var application = (HttpApplication)sender;
        this.Login(application);
    }

    void context_AuthenticateRequest(object sender, EventArgs e)
    {
        var application = (HttpApplication)sender;
        //if (application.Request.Path.StartsWith("/Public"))
        //    return;
        //this.CheckIsLogined(application);
    }

    bool Login(HttpApplication application)
    {
        var context = application.Context;
        if (context.Request.IsAuthenticated)
            return true;

        string isLogined = context.Request.QueryString.Get(LoginResultKey);
        //if (isLogined != "true")
        //    return new AuthenticateService().LoginByToken(context.Request.QueryString.Get(LoginTokenKey));
        //else
            return true;
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