using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// HttpHelper 的摘要说明
/// </summary>
public class HttpHelper
{
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
    public static string GetHtmlExByByPost(string postUrl, string paramData, Encoding dataEncode)
    {
        string ret = string.Empty;
        try
        {
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
            return null;
        }
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
}