 /*  作者：       tianzh
 *  创建时间：   2012/7/22 22:05:18
 *
 */
 /*  作者：       tianzh
 *  创建时间：   2012/7/22 15:34:14
 *
 */
using System.Data;
using System.Data.Objects;
using System.Collections.Generic;

namespace TZHSWEET.Common
{
    /// <summary>
    /// 用于存放过滤参数,比如一个是名称,一个是值,等价于sql中的Parameters
    /// </summary>
    public class FilterParam
    {
        public FilterParam(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
        /// <summary>
        /// 转化为ObjectParameter可变参数
        /// </summary>
        /// <returns></returns>
        public ObjectParameter ToObjParam()
        {
            ObjectParameter param = new ObjectParameter(this.Name,this.Value);
            return param;
        }
        /// <summary>
        /// 为查询语句添加参数
        /// </summary>
        /// <param name="commandText">查询命令</param>
        /// <returns></returns>
        public static string AddParameters(string commandText,IEnumerable<FilterParam> listfilter)
        {
            foreach (FilterParam param in listfilter)
            {
                if (param.Value.IsValidInput())
                {
                  commandText=commandText.Replace("@"+param.Name,"'"+ param.Value.ToString()+"'");
                }

            }
            return commandText;
        }
        /// <summary>
        /// 转化为ObjectParameter可变参数
        /// </summary>
        /// <param name="listfilter"></param>
        /// <returns></returns>
        public static ObjectParameter[] ConvertToListObjParam(IEnumerable<FilterParam> listfilter)
        {
            List<ObjectParameter> list = new List<ObjectParameter>();
            foreach (FilterParam param in listfilter)
            {
                list.Add(param.ToObjParam());
            }
            return list.ToArray();
        }

    }
}
