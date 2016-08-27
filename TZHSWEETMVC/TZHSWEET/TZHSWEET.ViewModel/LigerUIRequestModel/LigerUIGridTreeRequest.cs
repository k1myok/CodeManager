 /*  作者：       tianzh
 *  创建时间：   2012/7/21 11:00:31
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
    /// LigerUIGridTree 请求
    /// </summary>
   public class LigerUIGridTreeRequest
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
        /// 父级Id
        /// </summary>
        public string PidField { get; set; }
       /// <summary>
       /// 构造请求
       /// </summary>
       /// <param name="context"></param>
        public LigerUIGridTreeRequest(HttpContextBase context)
       {
           View = context.Request["view"];
           Where = context.Request["where"];
           IdField = context.Request["idfield"];
           PidField = context.Request["pidfield"];
       
       }
        
    }
}
