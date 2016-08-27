 /*  作者：       tianzh
 *  创建时间：   2012/7/21 11:02:58
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
    /// LigerUI Select请求
    /// </summary>
   public class LigerUISelectRequest
    {
          /// <summary>
        ///字段查看视图(暂时没做到)
        /// </summary>
        public string View { get; set; }
        /// <summary>
        /// 筛选器
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string IdField { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string TextField { get; set; }
       /// <summary>
       /// 以xx字段去除重复项
       /// </summary>
        public string Distinct { get; set; }
       /// <summary>
       /// 构造请求
       /// </summary>
       /// <param name="context"></param>
        public LigerUISelectRequest(HttpContextBase context)
       {
           View = context.Request["view"];
           Where = context.Request["where"];
           IdField = context.Request["idfield"];
           TextField = context.Request["textfield"];
           Distinct = context.Request["distinct"];
       }

       
    }
}
