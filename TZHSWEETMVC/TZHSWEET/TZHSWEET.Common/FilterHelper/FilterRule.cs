 /*  作者：       tianzh
 *  创建时间：   2012/7/22 22:05:45
 *
 */
 /*  作者：       tianzh
 *  创建时间：   2012/7/22 15:34:19
 *
 */
namespace TZHSWEET.Common
{
    public class FilterRule
    {
        /// <summary>
        /// 过滤规则
        /// </summary>
        public FilterRule()
        {
        }
        /// <summary>
        /// 过滤规则
        /// </summary>
        /// <param name="field">参数</param>
        /// <param name="value">值</param>
        public FilterRule(string field, object value)
            : this(field, value, "equal")
        {
        }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="field">参数</param>
        /// <param name="value">值</param>
        /// <param name="op">操作</param>
        public FilterRule(string field, object value, string op)
        {
            this.field = field;
            this.value = value;
            this.op = op;
        }
        /// <summary>
        /// 字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object value { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string op { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
    }
}
