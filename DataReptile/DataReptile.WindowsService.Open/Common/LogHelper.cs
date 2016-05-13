using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReptile.DataImport
{
    public class LogHelper
    {
        //public static async void WriteLog(string log, string logKind)
        //{
        //    var path = ConfigurationManager.AppSettings["LogsPath"] + "\\" + logKind;
        //    if (!System.IO.Directory.Exists(path))
        //        System.IO.Directory.CreateDirectory(path);
        //    var file = System.IO.Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".txt");
        //    try
        //    {
        //        System.IO.File.AppendAllText(file, log == null ? "空内容" : log);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

         public static async void WriteLog(string log, string logPath, string logKind)
        {
            var path = logPath + "\\" + logKind;
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            var file = System.IO.Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".txt");
            try
            {
                System.IO.File.AppendAllText(file, log == null ? "空内容" : log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
   }
}
