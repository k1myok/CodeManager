using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WXHelp;
using System.Web;


namespace WXHelp
{
    /// <summary>
    /// 调用帮助微信接口方法
    /// </summary>
   public class InterfaceHelp
    {
       /// <summary>
        /// 获取GetAccess_token
       /// </summary>
        /// <param name="appid">微信公众号appid</param>
        /// <param name="appsecret">微信公众号appsecret</param>
       /// <returns></returns>
       public static string GetAccess_token(string appid,string appsecret)
       {
           var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, appsecret);
           var result = HttpHelp.GetHtmlEx(url,Encoding.UTF8);
           return result;
       }
       /// <summary>
       /// 获取服务器IP列表
       /// </summary>
       /// <param name="access_token">接口调用获取IP列表</param>
       /// <returns></returns>
       public static string GetServiceIP(string access_token)
       {
           var url = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", access_token);
           var result = HttpHelp.GetHtmlEx(url,Encoding.UTF8);
           return result;
       }
      /// <summary>
      /// 网页赋权 根据code获取access_token
      /// </summary>
       /// <param name="appid">微信公众号appid</param>
       /// <param name="appsecret">微信公众号appsecret</param>
       /// <param name="code">微信获取页面的Code</param>
      /// <returns></returns>
       public static string GetAccess_token(string appid,string appsecret, string code)
       {
           var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, appsecret, code);
           var result = HttpHelp.GetHtmlEx(url,Encoding.UTF8);
           return result;
       }
       #region 编码回跳地址
       /// <summary>
       /// 编码回跳地址
       /// </summary>
       /// <param name="appid">微信公众号appidappid</param>
       /// <param name="url">微信回调网址</param>
       /// <returns></returns>
       public static string GetAuthorizaUrl(string appid, string url)
       {
            url = System.Web.HttpUtility.UrlEncode(url);
            url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=123", appid, url);
           return url;
       }
       /// <summary>
       /// 根据openid和access_token 获取用户个人的信息
       /// </summary>
       /// <param name="access_token">微信公众号access_token</param>
       /// <param name="openid">微信公众号openid</param>
       /// <returns></returns>
       public static string GetPersonalMsg(string access_token,string openid)
       {
           var url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
           var result=HttpHelp.GetHtmlEx(url, Encoding.UTF8);
           return result;
       }
       #endregion
    }
}
