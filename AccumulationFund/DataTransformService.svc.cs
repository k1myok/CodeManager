using SelectProvidentFundService.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SelectProvidentFundService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DataTransformService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DataTransformService.svc 或 DataTransformService.svc.cs，然后开始调试。
    public class DataTransformService : IDataTransformService
    {
        private static string uid = ConfigurationManager.AppSettings["uid"];
        private static string pwd = ConfigurationManager.AppSettings["pwd"];
        private static string mode = ConfigurationManager.AppSettings["mode"];
        public string GetAccountStatus(string idCard)
        {
            var trancode = "XA46L";
            string url = ConfigurationManager.AppSettings["productUrl"];
            if (mode == "product")
            {
                var args = GetXmlFile.setXML6(trancode,uid, Encryption.Md5(pwd),idCard);
                var result = HttpHelper.GetHtmlExByByPost(url, args, Encoding.UTF8);
                return result;
            }
            else
            {
                var path = System.Web.HttpRuntime.AppDomainAppPath;
                var fileName = path + @"\xml测试数据\公积金信息.xml";
                var result = System.IO.File.ReadAllText(fileName);
                return result;
            }         
        }

        public string GetProvidentFund(string idCard, string id, string trancode)
        { 
            string url = ConfigurationManager.AppSettings["productUrl"];
            if (mode == "product")
            {
                var args = GetXmlFile.setXML(trancode, uid, Encryption.Md5(pwd), idCard, id);
                var result = HttpHelper.GetHtmlExByByPost(url, args, Encoding.UTF8);
                return result;
            }
            else
            {
                var path = System.Web.HttpRuntime.AppDomainAppPath;                
                var fileName = path + @"\xml测试数据\公积金信息.xml";
                var result = System.IO.File.ReadAllText(fileName);
                return result;
            }         
        }
        public string GetProvidentFundDepositInfo(string idCard, string id, string page, string total, string pages, string trancode = "GJ08L1")
        {
            string url = ConfigurationManager.AppSettings["productUrl"];
            if (mode == "product")
            {
                var args = GetXmlFile.setXML5(trancode, uid, Encryption.Md5(pwd), idCard, id, page, total, pages);
                var result = HttpHelper.GetHtmlExByByPost(url, args, Encoding.UTF8);
                return result;
            }
            else
            {
                var path = System.Web.HttpRuntime.AppDomainAppPath;
                var fileName = path + @"\xml测试数据\公积金缴存信息.xml";
                var result = System.IO.File.ReadAllText(fileName);
                return result;
            }         
        }

        //个人贷款信息
        public string GetProvidentFundLoanInfo(string idCard, string trancode = "J005L4")
        {
            string url = ConfigurationManager.AppSettings["productUrl"];
            if (mode == "product")
            {
                var args = GetXmlFile.setXML2(trancode, uid, Encryption.Md5(pwd), idCard);
                var result = HttpHelper.GetHtmlExByByPost(url, args, Encoding.UTF8);
                return result;
            }
            else
            {
                var path = System.Web.HttpRuntime.AppDomainAppPath;
                var fileName = path + @"\xml测试数据\贷款信息.xml";
                var result = System.IO.File.ReadAllText(fileName);
                return result;
            }
        }
        //个人贷款还款信息
        public string GetProvidentFundRepayInfo(string loadNum, string page, string total, string pages, string trancode = "J021L1")
        {
            string url = ConfigurationManager.AppSettings["productUrl"];
            if (mode == "product")
            {
                var args = GetXmlFile.setXML3(trancode, uid, Encryption.Md5(pwd), loadNum, page, total, pages);
                var result = HttpHelper.GetHtmlExByByPost(url, args, Encoding.UTF8);
                return result;
            }
            else
            {
                var path = System.Web.HttpRuntime.AppDomainAppPath;
                var fileName = path + @"\xml测试数据\贷款还款信息.xml";
                var result = System.IO.File.ReadAllText(fileName);
                return result;
            }
        }


        public CheckCodeResult SendCheckCode(string custAcNo, string paperId, string mobile)
        {
            var random = new Random();
            var msgcheckcode = random.Next(100000, 999999).ToString();
            var sendMessage = string.Format(ConfigurationManager.AppSettings["MessageFormat"], msgcheckcode);
            var args = GetXmlFile.setXML4("DX06L", ConfigurationManager.AppSettings["uid"], Encryption.Md5(ConfigurationManager.AppSettings["pwd"]), custAcNo, paperId, mobile, sendMessage);
            string url = ConfigurationManager.AppSettings["productUrl"];
            var message = HttpHelper.GetHtmlExByByPost(url, args, Encoding.UTF8);
            var result = (message != null && message.Contains("验证通过"));
            var log = string.Format("request:{0};result:{1};url:{2}", args, message, url);
            var file = System.IO.Path.Combine(ConfigurationManager.AppSettings["logfile"], DateTime.Now.ToString("yyyyMMddhh") + ".txt");
            System.IO.File.AppendAllText(file, log);

            message = message.Remove(0,message.IndexOf("<message>") + 9);
            message = message.Remove(message.IndexOf("</message>"));
            return new CheckCodeResult() {
                Result = result,
                CheckCode = result ? msgcheckcode : "",
                Message = message
            };
        }
    }
}
