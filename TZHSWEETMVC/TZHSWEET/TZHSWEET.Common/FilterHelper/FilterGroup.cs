 /*  作者：       tianzh
 *  创建时间：   2012/7/22 22:05:08
 *
 */

namespace TZHSWEET.Common
{
    using System.Collections.Generic;
    /// <summary>
    /// 对应前台 ligerFilter 的检索规则数据
    /// </summary>
    public class FilterGroup
    {
        /// <summary>
        /// 规则
        /// </summary>
        public IList<FilterRule> rules { get; set; }
        /// <summary>
        /// 操作符
        /// </summary>
        public string op { get; set; }
        /// <summary>
        /// 条件组
        /// </summary>
        public IList<FilterGroup> groups { get; set; }
    }
}
