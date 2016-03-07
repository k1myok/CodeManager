using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShareService.Service.ARR
{
    public class ARRServer
    {
        public ARRServer(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }
        public string Host { get; set; }

        public int Port { get; set; }

        public XElement ConvertToXElement()
        {
            var template = System.IO.Path.Combine(ARRConfig.TemplatePath, "APPServer.xml");
            var content = System.IO.File.ReadAllText(template);
            var xml = string.Format(content, this.Host, this.Port);
            var element = XElement.Parse(xml);
            return element;
        }
    }
}
