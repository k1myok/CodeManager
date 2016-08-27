 /*  作者：       tianzh
 *  创建时间：   2012/7/21 9:19:53
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TZHSWEET.ViewModel
{
   public class LigerUIGrid
    {
       /// <summary>
       /// 返回的记录
       /// </summary>
       public object Rows { get; set; }
       /// <summary>
       /// 总个数
       /// </summary>
       public int Total { get; set; }
       public override string ToString()
       {
           return JsonConvert.SerializeObject(this, Formatting.Indented,
 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, });

       }
    }
}
