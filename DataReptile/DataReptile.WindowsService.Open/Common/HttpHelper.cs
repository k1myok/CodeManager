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
    public static System.DateTime ConvertIntDateTime(double milliseconds)
    {
        System.DateTime time = System.DateTime.MinValue;
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        time = startTime.AddMilliseconds(milliseconds);
        return time;
    } 
}