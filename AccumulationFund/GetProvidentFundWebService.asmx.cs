using SelectProvidentFundService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Configuration;

namespace SelectProvidentFundService
{
    /// <summary>
    /// GetProvidentFundWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://app.china-ccw.com:8011/PFWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GetProvidentFundWebService : System.Web.Services.WebService
    {
        private static string uid = ConfigurationManager.ConnectionStrings["uid"].ToString();
        private static string pwd = ConfigurationManager.ConnectionStrings["pwd"].ToString();
        private static string url = ConfigurationManager.ConnectionStrings["url"].ToString();

        /// <summary>
        /// 根据身份证号码、公积金编号和交易号码获取用户信息
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <param name="id">公积金编号</param>
        /// <param name="trancode">交易号码</param>
        /// <returns></returns>
        [WebMethod]
        public string GetProvidentFund(string idCard, string id, string trancode)
        {
            var args = GetXmlFile.setXML(trancode, uid, Encryption.Md5(pwd), idCard, id);

            var result = HttpHelper.GetHtmlExByByPost(url, args, Encoding.UTF8);

            return result;
        }
    }
}
