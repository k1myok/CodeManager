using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region 获取Access_token 接口返回的信息
/// <summary>
/// 获取Access_token 接口返回的信息
/// </summary>
    public  class Access_token
    {
        /// <summary>
        /// access_token
        /// </summary>
       public string access_token { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
       public string expires_in { get; set; }
    }
#endregion
#region 获取Access_token 接口返回的信息
/// <summary>
    /// 获取Access_token 接口返回的信息
/// </summary>
public class Access_tokenResult
      { 
    /// <summary>
          /// access_token
    /// </summary>
          public string access_token{get;set;}
    /// <summary>
          /// expires_in
    /// </summary>
          public string expires_in{get;set;}
    /// <summary>
    /// refresh_token
    /// </summary>
          public string refresh_token{get;set;}
    /// <summary>
          /// openid
    /// </summary>
          public string openid{get;set;}
    /// <summary>
    /// scope
    /// </summary>
          public string scope{get;set;}
      }
      #endregion
#region 个人账号的信息
/// <summary>
/// 个人账号的信息
/// </summary>
 public class  PMSG
 {
     /// <summary>
     /// 用户OPENID
     /// </summary>
     public string openid{get;set;}
     /// <summary>
     /// 用户昵称
     /// </summary>
     public string nickname{get;set;}
     /// <summary>
     /// 用户性别
     /// </summary>
     public string sex{get;set;}
  /// <summary>
  /// 用户的城市
  /// </summary>
      public string  city{get;set;}
     /// <summary>
     /// 用户所在国家
     /// </summary>
      public string  country{get;set;}
     /// <summary>
     /// 用户的头像
     /// </summary>
      public string  headimgurl{get;set;}
     /// <summary>
     /// 用户的群组
     /// </summary>
      public  string[]  privilege{get;set;}
     /// <summary>
      /// 用户的unionid
     /// </summary>
      public string unionid{get;set;}
 }
      #endregion



