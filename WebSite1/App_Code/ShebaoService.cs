using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class ShebaoService
{
    // private static  var client = new Shebao.ShbServClient();
	// 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
	// 要创建返回 XML 的操作，
	//     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
	//     并在操作正文中包括以下行:
	//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
	[OperationContract]
	public void DoWork()
	{
		// 在此处添加操作实现
		return;
	}
    [WebGet]
    [OperationContract]
    public string GetResult(string ID, string IDCard)
   { 
     var client = new Shebao.ShbServClient();
     var result = client.si010201(ID,IDCard,8,1);
     XElement xml = XElement.Load(result);
     var query = xml.Descendants("row");
     foreach (var item in query)
     {
       var aab004=item.Attribute("aab004").Value;
     }
     
     return result;
   }
}
