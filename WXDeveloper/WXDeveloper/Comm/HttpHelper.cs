﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


/// <summary>
/// HttpHelper 的摘要说明
/// </summary>
public class HttpHelper
{
    //模拟Get提交
    public static string GetHtmlEx(string url, Encoding encoding)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "get";
        request.UserAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30)";
        WebResponse response = request.GetResponse();
        Stream responseStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(responseStream, encoding);
        String html = reader.ReadToEnd();
        response.Close();

        return html;
    }
    //模拟post提交
    public static string GetHtmlExByByPost(string postUrl, string paramData, Encoding dataEncode)
    {
        string ret = string.Empty;
        try
        {
            //WriteToLog(string.Format("GetHtmlExByByPost:{0},{1}", postUrl, paramData));
            byte[] byteArray = dataEncode.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            //webReq.ContentType = "application/x-www-form-urlencoded";

            webReq.UserAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30)";

            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
        }
        catch (Exception ex)
        {
            WriteToLog(ex.Message);
            return null;
        }
        //WriteToLog(string.Format("result:{0}",ret ));
        return ret;
    }
    public static string GetHtmlExByByPost(string postUrl, string paramData, Encoding dataEncode, Action<HttpWebRequest> action)
    {
        string ret = string.Empty;
        try
        {
            byte[] byteArray = dataEncode.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType ="application/x-www-form-urlencoded";
            webReq.UserAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30)";
            //webReq.Accept ="image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            webReq.ContentLength = byteArray.Length;
            if (action != null)
                action(webReq);
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
        }
        catch (Exception ex)
        {
            return null;
        }
        return ret;
    }
    public static System.DateTime ConvertIntDateTime(double milliseconds)
    {
        System.DateTime time = System.DateTime.MinValue;
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        time = startTime.AddMilliseconds(milliseconds);
        return time;
    }
    //写日志
    public static void WriteToLog(string logMessage)
    {
        try
        {
            var path = ConfigurationManager.AppSettings["LogPath"];
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var logFilePath = string.Format("{0}\\{1}.txt", path, DateTime.Now.ToString("yyyy_MM_dd hh_mm"));
            System.IO.File.AppendAllText(logFilePath, logMessage);
        }
        catch (Exception ex)
        {

        }
    }
    public string SendMessage(string temp)
    {
        var appId = "wx33cfa168b97bde1b";
        var secret = "88626508ef000750214526005b12cb7c";
        var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, secret);
        string access = HttpHelper.GetHtmlEx(url, Encoding.UTF8);


        JObject model = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(access);

        var access_token = model.GetValue("access_token");


        // var jsonItems = (Newtonsoft.Json.JsonConvert.DeserializeObject(access_token) as Newtonsoft.Json.Linq.JObject).Children().OfType<Newtonsoft.Json.Linq.JProperty>();
        //var tokenItem = jsonItems.FirstOrDefault(p => p.Name == "access_token").Value as Newtonsoft.Json.Linq.JValue;
        //var tokenValue = tokenItem.Value.ToString();
        var openID = "oMpRLuIAGvDMAjObxiU1s8ESdrmU";
        var json = "{\"touser\":\"" + openID + "\",\"template_id\":\"ClhlbsSTn4rvojun9RaMr9Bep_X9GhRKgbg7oEU9rq4\",\"url\":\"http://app.china-ccw.com:8011/CityService/Cityservice.weixin/Health/getHospitallist.html\",\"data\":{\"first\":{\"value\":\"你已经挂号成功，详细信息如下:\",\"color\":\"#173177\" },\"keyword1\":{\"value\":\"测试数据\",\"color\":\"#173177\" },\"keyword2\":{\"value\":\"苏州第一人民医院\",\"color\":\"#173177\"},\"keyword3\":{\"value\":\"测试科\",\"color\":\"#173177\" },\"keyword4\":{\"value\":\"测试人员\",\"color\":\"#173177\" },\"remark\":{\"value\":\"请按时就诊\" }}}";
        var ur = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", access_token);
        var result = HttpHelper.GetHtmlExByByPost(ur, json, Encoding.UTF8);
        return result;
    }

}