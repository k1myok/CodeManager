/*  作者：       tianzh
*  创建时间：   2012/7/21 23:51:38
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;

namespace TZHSWEET.IBLL
{
    public interface IOperateLogService : IBaseService<tbOperateLog>
    {
        /// <summary>
        /// 获取树形的Grid的json数据
        /// </summary>
        /// <param name="gridData"></param>
        /// <returns></returns>
        LigerUIGrid GetAllLogsToGrid(LigerUIGridRequest gridData);
         /// <summary>
        /// 写系统操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
         bool WriteOperateLog(ViewModelOperateLog log);
          /// <summary>
        /// 清空三个月以前的日志
        /// </summary>
        /// <returns></returns>
         bool DeleteThreeMonthLogs();
    }
}
