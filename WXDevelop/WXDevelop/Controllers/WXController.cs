using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXHelp;
using System.Management;
using System.Configuration;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace WXDevelop.Controllers
{
    public class WXController : Controller
    {
        //
        // GET: /WX/
       private  static string  appid = ConfigurationManager.AppSettings["appid"].ToString();
       private static string appsecret = ConfigurationManager.AppSettings["AppSecret"].ToString();

        public ActionResult Index()
        {
            var result = InterfaceHelp.GetAccess_token(appid, appsecret);
            var model =JsonConvert.DeserializeObject<Access_token>(result);
            result =InterfaceHelp.GetServiceIP( model.access_token);
            var mode = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            return View(mode);
        }
        #region 静默获取OpenID
        public ActionResult BaseCallBack()
        {
            //var appid = ConfigurationManager.AppSettings["appid"].ToString();
            // var appsecret = ConfigurationManager.AppSettings["AppSecret"].ToString();
              var url=ConfigurationManager.AppSettings["URL"].ToString();
              url = url + "wx/GetOPenID";
              url =InterfaceHelp.GetAuthorizaUrl(appid,url);
              this.WriteLog(url,"WX","WXLog.txt");
             return Redirect(url);  
        }
         public ActionResult GetOPenID(string code)
        {
            var result =InterfaceHelp.GetAccess_token(appid, appsecret, code);
            this.WriteLog(result, "WX", "WXLog.txt");
            var model = JsonConvert.DeserializeObject<Access_tokenResult>(result);
            return View(model);

        }
        #endregion

         #region 用户授权获取用户的信息

         public ActionResult BaseCallBack1()
         {
             var url = ConfigurationManager.AppSettings["URL"].ToString();
             url = url + "wx/GetMsg";
             url =InterfaceHelp.GetAuthorizaUrl(appid, url);
             this.WriteLog(url, "WX", "WXLog.txt");
             return Redirect(url);
         }
         public ActionResult GetMsg(string code)
         {
             var result =InterfaceHelp.GetAccess_token(appid, appsecret, code);
             this.WriteLog(result, "WX", "GetMsg.txt");
             var model = JsonConvert.DeserializeObject<Access_tokenResult>(result);
             var tempresult = InterfaceHelp.GetPersonalMsg(model.access_token, model.openid);
             this.WriteLog(tempresult,"wx","Personmsg.txt");
             var mode = new PMSG();
             try
             {
                 mode = JsonConvert.DeserializeObject<PMSG>(tempresult);
                 this.WriteLog(mode.ToString(),"model","model.txt");
             }
             catch (Exception ex)
             {
                 this.WriteLog(ex.Message,"Wx","error.txt");
             }
             // this.WriteLog(mode.ToString(),"Wx","model.txt");
             return View(mode);
         }
         public ActionResult Test()
         {
             var paht = Server.MapPath("~/Resource/Personmsg.txt");
             var fd = System.IO.File.ReadAllText(paht);
             var mode = JsonConvert.DeserializeObject<PMSG>(fd); ;
             return View(mode);
         }



         #endregion

         public void WriteLog(string log, string kind, string fileName = "")
         {
             try
             {
                 var path = ConfigurationManager.AppSettings["LogsPath"] + "\\" + kind;
                 if (!System.IO.Directory.Exists(path))
                     System.IO.Directory.CreateDirectory(path);

                 var file = System.IO.Path.Combine(path, string.IsNullOrEmpty(fileName) ? (DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".txt") : fileName);
                 System.IO.File.AppendAllText(file, log + Environment.NewLine);
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }
         }
         public ActionResult List()
          {
              return View();
         }
    }

}

