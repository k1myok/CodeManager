 /*  作者：       tianzh
 *  创建时间：   2012/7/22 12:12:52
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
    /// 删除命令请求信息
    /// </summary>
   public class RequestBase
    {
       public string ID { get; set; }
       public RequestBase(HttpContextBase context)
       {
           ID = context.Request["ID"];
       }
    }
}
