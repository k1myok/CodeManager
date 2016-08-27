 /*  作者：       tianzh
 *  创建时间：   2012/7/19 18:16:08
 *
 */
 /*  作者：       tianzh
 *  创建时间：   2012/7/16 15:31:37
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace TZHSWEET.ViewModel
{
    public class LigerUIGridRequest
    {

        private string sortOrder;

        private int pageSize;
        /// <summary>
        /// 字段查看视图(暂时没做到)
        /// </summary>
        public string View
        {
            get;
            set;
        }
        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string SortName { get; set; }
        /// <summary>
        /// 排序规则
        /// </summary>
        public string SortOrder
        {
            get
            {
                if (this.sortOrder == "desc")
                    return this.sortOrder;
                else
                    return "asc";
            }

            set
            {
                this.sortOrder = value;
            }
        }
        private int pageNumber;
        /// <summary>
        /// 页号
        /// </summary>
        public int PageNumber
        {
            get
            {
                if (this.pageNumber <= 0)
                    return 1;
                else
                    return pageNumber;
            }
            set
            {
                if (value <= 0)
                    pageNumber = 1;
                else
                    pageNumber = value;
            }
        }
        /// <summary>
        /// 每页的多少条数据
        /// </summary>
        public int PageSize 
        {
            get
            {
                return this.pageSize;
            }
            set
            { 
               this.pageSize= (value==0?10:value);
            }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Where { get; set; }
        /// <summary>
        /// 初始化读取信息
        /// </summary>
        /// <param name="context"></param>
        public LigerUIGridRequest(HttpContextBase context)
        {
            this.View = context.Request["view"];
            this.SortName= context.Request["sortname"];
           this.SortOrder = context.Request["sortorder"]=="desc"?"desc":"asc";
           this.PageNumber =Convert.ToInt32(context.Request["page"]);
           this.PageSize =Convert.ToInt32(context.Request["pagesize"]);
            this.Where = context.Request["where"];

        }
    }
}
