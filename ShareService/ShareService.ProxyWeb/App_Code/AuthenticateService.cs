using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class AuthenticateService
{
	// 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
	// 要创建返回 XML 的操作，
	//     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
	//     并在操作正文中包括以下行:
	//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
	[OperationContract]
	public bool Login(string userName, string passWord)
	{
        FormsAuthentication.SetAuthCookie("admin", false);
        return true;
        if (Membership.ValidateUser("admin", "123456"))
        {
            FormsAuthentication.SetAuthCookie("admin", true);
            return true;
        }
        return false;
    }

    [OperationContract]
    public bool LoginByToken(string loginToken)
    {
        if (string.IsNullOrEmpty(loginToken))
            return false;

        return true;
    }

	// 在此处添加更多操作并使用 [OperationContract] 标记它们
}

public class CustomUser
{
    public CustomUser(string userName, string password)
    {
        this.UserName = userName;
        this.Password = password;
    }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string BindingURL { get; set; }

    public string ConvertToToken()
    {
        return string.Format("{0}_{1}", this.UserName, this.Password);
    }
    public static CustomUser TokenToUser(string token)
    {
        return new CustomUser(token.Split('_')[0], null);
    }

    static List<CustomUser> users;

    public static List<CustomUser> Users
    {
        get
        {
            if(users == null)
            {
                users = new List<CustomUser>() {
                    new CustomUser("admin", "123456") { BindingURL = "http://localhost:801/arcgis/services/TOCC_GIS/BusSite" },
                    new CustomUser("zhangsan", "123456") { BindingURL = "http://localhost:801/arcgis/services/TOCC_GIS/BusSite" },
                    new CustomUser("lisi", "123456") { BindingURL = "http://localhost:801/arcgis/rest/services/Finance" }
                };
            }
            return users;
        }
    }

}
