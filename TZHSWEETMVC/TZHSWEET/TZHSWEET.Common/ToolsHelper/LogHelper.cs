 /*  作者：       tianzh
 *  创建时间：   2012/7/21 21:12:05
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TZHSWEET.Common
{
  public  class LogHelper
    {
        public  LogHelper()
        {
            
        }

        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 

        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        /// <summary>
        /// 设置文件路径
        /// </summary>
        /// <param name="configFile"></param>
        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }
        /// <summary>
        /// 写系统信息日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="Error"></param>
        /// <param name="se"></param>
        public static void WriteLog(string Error, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(Error, se);
            }
        }
    }
}