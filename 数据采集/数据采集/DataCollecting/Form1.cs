using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Collections;
using System.Xml;
using System.IO.Compression;
using HtmlAgilityPack;

namespace DataCollecting
{
    public partial class richTextBox1 : Form
    {
        public richTextBox1()
        {
            InitializeComponent();
        }
        //private WebDownloader m_wd = new WebDownloader();
        List<string> list = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            string url = GetHttpWebRequest(this.txtUrl.Text.Trim());
            txtHttp.Text = url;
        }
        /// <summary>
        /// 抓取网页内容
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        //private string GetHttpWebRequest(string url)
        //{
        //    Uri uri = new Uri(url);
        //    WebRequest webReq = WebRequest.Create(uri);
        //   // WebResponse webRes = webReq.GetResponse();
        //    HttpWebRequest myReq = (HttpWebRequest)webReq;
        //    myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
        //    myReq.Accept = "*/*";
        //    myReq.KeepAlive = true;
        //    myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
        //    HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
        //    Stream receviceStream = result.GetResponseStream();
        //    StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
        //    string strHTML = readerOfStream.ReadToEnd();
        //    readerOfStream.Close();
        //    receviceStream.Close();
        //    result.Close();
        //    return strHTML;
        //}
        /*
         当异常发生事后，WebException 中不仅有 StatusCode 标志着 HTTP 的错误代码，
         * 而且它的 Response 属性还包含由服务器发送的 WebResponse
         */
        //private string GetHttpWebRequest(string url)
        //{
        //    Uri uri = new Uri(url);
        //    WebRequest webReq = WebRequest.Create(uri);
        //    // WebResponse webRes = webReq.GetResponse();
        //    HttpWebRequest myReq = (HttpWebRequest)webReq;
        //    myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
        //    myReq.Accept = "*/*";
        //    myReq.KeepAlive = true;
        //    myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
        //    //HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
        //    HttpWebResponse result;
        //    try
        //    {
        //        result = (HttpWebResponse)myReq.GetResponse();
        //    }
        //    catch (WebException ex)
        //    {
        //        result=(HttpWebResponse)ex.Response;
        //    }
        //    Stream receviceStream = result.GetResponseStream();
        //    StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
        //    string strHTML = readerOfStream.ReadToEnd();
        //    readerOfStream.Close();
        //    receviceStream.Close();
        //    result.Close();
        //    return strHTML;
        //}
        private string GetHttpWebRequest(string url)
        {
            HttpWebResponse result;
            string strHTML = string.Empty;
            try
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                WebResponse webRes = webReq.GetResponse();

                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                result = (HttpWebResponse)myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = readerOfStream.ReadToEnd();
                readerOfStream.Close();
                receviceStream.Close();
                result.Close();
            }
            catch
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                //result = (HttpWebResponse)myReq.GetResponse();
                try
                {
                    result = (HttpWebResponse)myReq.GetResponse();
                }
                catch (WebException ex)
                {
                    result = (HttpWebResponse)ex.Response;
                }
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("gb2312"));
                strHTML = readerOfStream.ReadToEnd();
                readerOfStream.Close();
                receviceStream.Close();
                result.Close();
            }
            return strHTML;
        }



        /// <summary>
        /// 提取HTML代码中的网址
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        private static List<string> GetHyperLinks(string htmlCode, string url)
        {
            ArrayList al = new ArrayList();
            bool IsGenxin = false;
            StringBuilder weburlSB = new StringBuilder();//SQL
            StringBuilder linkSb = new StringBuilder();//展示数据
            List<string> Weburllistzx = new List<string>();//新增
            List<string> Weburllist = new List<string>();//旧的
            string ProductionContent = htmlCode;
            Regex reg = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+/?");
            string wangzhanyuming = reg.Match(url, 0).Value;
            MatchCollection mc = Regex.Matches(ProductionContent.Replace("href=\"/", "href=\"" + wangzhanyuming).Replace("href='/", "href='" + wangzhanyuming).Replace("href=/", "href=" + wangzhanyuming).Replace("href=\"./", "href=\"" + wangzhanyuming), @"<[aA][^>]* href=[^>]*>", RegexOptions.Singleline);
            int Index = 1;
            foreach (Match m in mc)
            {
                MatchCollection mc1 = Regex.Matches(m.Value, @"[a-zA-z]+://[^\s]*", RegexOptions.Singleline);
                if (mc1.Count > 0)
                {
                    foreach (Match m1 in mc1)
                    {
                        string linkurlstr = string.Empty;
                        linkurlstr = m1.Value.Replace("\"", "").Replace("'", "").Replace(">", "").Replace(";", "");
                        weburlSB.Append("$-$");
                        weburlSB.Append(linkurlstr);
                        weburlSB.Append("$_$");
                        if (!Weburllist.Contains(linkurlstr) && !Weburllistzx.Contains(linkurlstr))
                        {
                            IsGenxin = true;
                            Weburllistzx.Add(linkurlstr);
                            linkSb.AppendFormat("{0}<br/>", linkurlstr);
                        }
                    }
                }
                else
                {
                    if (m.Value.IndexOf("javascript") == -1)
                    {
                        string amstr = string.Empty;
                        string wangzhanxiangduilujin = string.Empty;
                        wangzhanxiangduilujin = url.Substring(0, url.LastIndexOf("/") + 1);
                        amstr = m.Value.Replace("href=\"", "href=\"" + wangzhanxiangduilujin).Replace("href='", "href='" + wangzhanxiangduilujin);
                        MatchCollection mc11 = Regex.Matches(amstr, @"[a-zA-z]+://[^\s]*", RegexOptions.Singleline);
                        foreach (Match m1 in mc11)
                        {
                            string linkurlstr = string.Empty;
                            linkurlstr = m1.Value.Replace("\"", "").Replace("'", "").Replace(">", "").Replace(";", "");
                            weburlSB.Append("$-$");
                            weburlSB.Append(linkurlstr);
                            weburlSB.Append("$_$");
                            if (!Weburllist.Contains(linkurlstr) && !Weburllistzx.Contains(linkurlstr))
                            {
                                IsGenxin = true;
                                Weburllistzx.Add(linkurlstr);
                                linkSb.AppendFormat("{0}<br/>", linkurlstr);
                            }
                        }
                    }
                }
                Index++;
            }
            return Weburllistzx;
        }
        /// <summary>
        /// // 把网址写入xml文件
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="alHyperLinks"></param>
        private static void WriteToXml(string strURL, List<string> alHyperLinks)
        {
            XmlTextWriter writer = new XmlTextWriter(@"D:\HyperLinks.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument(false);
            writer.WriteDocType("HyperLinks", null, "urls.dtd", null);
            writer.WriteComment("提取自" + strURL + "的超链接");
            writer.WriteStartElement("HyperLinks");
            writer.WriteStartElement("HyperLinks", null);
            writer.WriteAttributeString("DateTime", DateTime.Now.ToString());
            foreach (string str in alHyperLinks)
            {
                string title = GetDomain(str);
                string body = str;
                writer.WriteElementString(title, null, body);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
        /// <summary>
        /// 获取网址的域名后缀
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        private static string GetDomain(string strURL)
        {
            string retVal;
            string strRegex = @"(\.com/|\.net/|\.cn/|\.org/|\.gov/)";
            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
            Match m = r.Match(strURL);
            retVal = m.ToString();
            strRegex = @"\.|/$";
            retVal = Regex.Replace(retVal, strRegex, "").ToString();
            if (retVal == "")
                retVal = "other";
            return retVal;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list = GetHyperLinks(txtHttp.Text, txtUrl.Text.Trim());
            //list = getNewsTitle(txtUrl.Text.Trim());
            txtHttp.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                txtHttp.Text += list[i] + "\t" + "\r\n";
            }
            WriteToXml(txtUrl.Text.Trim(), list);
        }



        /// <summary>
        /// 获取标题
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string GetTitle(string html)
        {
            string titleFilter = @"<title>[\s\S]*?</title>";
            string h1Filter = @"<h1.*?>.*?</h1>";
            string clearFilter = @"<.*?>";

            string title = "";
            Match match = Regex.Match(html, titleFilter, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                title = Regex.Replace(match.Groups[0].Value, clearFilter, "");
            }

            // 正文的标题一般在h1中，比title中的标题更干净
            match = Regex.Match(html, h1Filter, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string h1 = Regex.Replace(match.Groups[0].Value, clearFilter, "");
                if (!String.IsNullOrEmpty(h1) && title.StartsWith(h1))
                {
                    title = h1;
                }
            }
            return title;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            list = GetHyperLinks(txtHttp.Text, txtUrl.Text.Trim());
            txtTitle.Clear();
            string html = "";
            string title = "";
            foreach (var item in list)
            {
                html = GetHttpWebRequest(item);
                title = GetTitle(html);
                txtTitle.Text += title + "\t" + "\r\n";
            }
        }


        //private void GetTitle()
        //{
        //    string strContent
        //        = m_wd.GetPageByHttpWebRequest(this.textBoxUrl.Text, Encoding.UTF8);
        //    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument
        //    {
        //        OptionAddDebuggingAttributes = false,
        //        OptionAutoCloseOnEnd = true,
        //        OptionFixNestedTags = true,
        //        OptionReadEncoding = true
        //    };

        //    htmlDoc.LoadHtml(strContent);
        //    string strTitle = "";
        //    HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//title");
        //    // Extract Title
        //    if (!Equals(nodes, null))
        //    {
        //        strTitle = string.Join(";", nodes.
        //            Select(n => n.InnerText).
        //            ToArray()).Trim();
        //    }
        //    strTitle = strTitle.Replace("博客园", "");
        //    strTitle = Regex.Replace(strTitle, @"[|/\;:*?<>&#-]", "").ToString();
        //    strTitle = Regex.Replace(strTitle, "[\"]", "").ToString();
        //    this.textBoxTitle.Text = strTitle.TrimEnd();
        //}
    }
}
