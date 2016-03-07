using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Login2_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if(this.Login2.UserName == "admin" && this.Login2.Password == "123456")
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
                cookie.Value = "";
            FormsAuthentication.SetAuthCookie("admin", false);
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(Request.QueryString["ReturnUrl"], false);
        }
    }
}