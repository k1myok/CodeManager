 /*  作者：       tianzh
 *  创建时间：   2012/7/23 21:42:18
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TZHSWEET.ViewModel
{
    /// <summary>
    /// 用户请求
    /// </summary>
   public class UserRequest
    {
       public string UserName { get; set; }
       public string Password { get; set; }
       public string OldPassord { get; set; }
       public string NewPassword { get; set; }
       public string CheckCode { get; set; }
       public UserRequest(HttpContextBase context)
       {
           UserName = context.Request["UserName"];
           Password = context.Request["Password"];
           OldPassord = context.Request["OldPassword"];
           NewPassword = context.Request["NewPassword"];
           CheckCode = context.Request["CheckCode"];
       }
       /// <summary>
       /// 验证用户名请求构造
       /// </summary>
       /// <param name="context"></param>
       /// <param name="IsCheckUserName"></param>
       public UserRequest(HttpContextBase context, bool IsCheckUserName)
       {
           if (IsCheckUserName)
           {
               UserName = context.Request["username"];
           }
       }

    }
}
