using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace TZHSWEET.Common
{
    /// <summary>
    /// 用webClient下载文件
    /// </summary>
    public partial class DownFile
    {
        #region 生成静态页面
        /// <summary>
        /// 生成静态页面
        /// 调用实例:
        ///   Common.DownFile webclient = new Common.DownFile();
        ///    string RequestVirtualUrl= "/News/ViewNews.aspx?NewsId="+Info.Id;
        ///   string SaveVirtualPath = "~/News/" + Info.Id + ".htm";
        ///    webclient.CreateStaticByWebClient(RequestVirtualUrl, SaveVirtualPath);
        /// </summary>
        /// <param name="VirtualRequestUrl">要请求的虚拟路径,例如: "/News/ViewNews.aspx?NewsId="+Info.Id;</param>
        /// <param name="SaveVirtualPath">要保存的虚拟路径,例如:"~/News/" + Info.Id + ".htm";</param>
        public static void CreateStaticByWebClient(string VirtualRequestUrl, string SaveVirtualPath)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            //通过WebClient向服务器发Get请求,把服务器返回的html内容保存到磁盘上,以后用户直接请html文件请求.
            string AppVirtualPath = HttpContext.Current.Request.ApplicationPath;
            //由于网站应用程序虚拟目录是/czcraft,而传递过来/News是正确的,
            //但是发布到iis上面,虚拟路径就是/,而传递过来的确实/News,路径就出错了,

            if (AppVirtualPath == "/")
            {
                AppVirtualPath = "";
            }
            string FilePath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + AppVirtualPath + VirtualRequestUrl;

            //保存路径
            string SaveFilePath = HttpContext.Current.Server.MapPath(SaveVirtualPath);

            //下载并保存文件
            wc.DownloadFile(FilePath, SaveFilePath);

        } 
        #endregion
     
    }
}
