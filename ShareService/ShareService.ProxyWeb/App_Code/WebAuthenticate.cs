using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Linq;


/// <summary>
/// UrlReWriter 的摘要说明
/// </summary>
public class WebAuthenticate : IHttpModule, System.Web.SessionState.IRequiresSessionState
{
    public const string LoginTokenKey = "LoginToken";

    public void Init(HttpApplication context)
    {
        context.BeginRequest += context_BeginRequest;
        context.AuthenticateRequest += context_AuthenticateRequest;
    }

    void context_BeginRequest(object sender, EventArgs e)
    {
        var application = (HttpApplication)sender;
        if (application.Context.Request.Url.AbsoluteUri.Contains("Public/"))
            return;

        string userName = "";
        var cookie = application.Request.Cookies[FormsAuthentication.FormsCookieName];
        if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
        {
            userName = FormsAuthentication.Decrypt(cookie.Value).Name;
        }
        var token = application.Request.QueryString.Get(LoginTokenKey);
        if (!string.IsNullOrEmpty(token))
        {
            var user = CustomUser.TokenToUser(token);
            userName = user.UserName;
        }

        if(string.IsNullOrEmpty(userName))
        {
            FormsAuthentication.SignOut(); FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            var service = CustomUser.Users.FirstOrDefault(p => p.UserName == userName);
            if(service != null && application.Request.Url.AbsoluteUri.StartsWith(service.BindingURL))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }

    void context_AuthenticateRequest(object sender, EventArgs e)
    {
    }

    public void Dispose()
    {
    }
}