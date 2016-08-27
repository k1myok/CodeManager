 /*  作者：       tianzh
 *  创建时间：   2012/7/21 23:50:00
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TZHSWEET.IDao
{
    public interface IOperateLogDao<T> : IBaseDao<T> where T : class
    {
           /// <summary>
        /// 按照月份删除
        /// </summary>
        /// <param name="month">删除多少月份以前的数据</param>
        /// <returns></returns>
        bool DeleteByMonth(int month);
    }
}
