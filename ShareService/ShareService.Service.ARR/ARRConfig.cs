using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareService.Service.ARR
{
    public class ARRConfig
    {
        public static string TemplatePath = ConfigurationManager.AppSettings["TemplatePath"];

        public static string Host = ConfigurationManager.AppSettings["Host"];

        public static int Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
    }
}
