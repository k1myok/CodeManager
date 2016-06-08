using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //通过Log4Net往日志文件里面写数据
            //让配置启用
            log4net.Config.XmlConfigurator.Configure();
            //
            ILog log = log4net.LogManager.GetLogger("HYL");

            log.Debug("sdsds");
            log.Fatal("sdssds");
            //
        }
    }
}
