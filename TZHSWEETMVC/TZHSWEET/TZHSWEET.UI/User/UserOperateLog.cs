 /*  作者：       tianzh
 *  创建时间：   2012/7/22 9:44:25
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.BLL;
using TZHSWEET.IBLL;
using TZHSWEET.ViewModel;

namespace TZHSWEET.UI
{
   public class UserOperateLog
    {
       /// <summary>
       /// 静态
       /// </summary>
       public static readonly IOperateLogService log = new OperateLogService();
       /// <summary>
       /// 写操作日志
       /// </summary>
       /// <param name="description">日志信息</param>
       /// <returns>执行状态</returns>
       public static bool WriteOperateLog(string description)
       {
           SysCurrentUser user = new SysCurrentUser(true);
           return log.WriteOperateLog(new ViewModelOperateLog(description));
       }
    }
}
