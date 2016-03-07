using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShareService.Service.ARR
{
    public class ARRFarm
    {
        public Guid Code { get; set; }

        public string Name { get; set; }

        public List<ARRServer> Servers { get; set; }

        public XElement ConvertToXElement()
        {
            var farm = new XElement(XName.Get("webFarm"));
            farm.SetAttributeValue(XName.Get("name"), this.Name);
            if(this.Servers == null || this.Servers.Count == 0)
                return farm;

            foreach (var server in this.Servers)
            {
                if (farm.FindElement(p => p.Name.LocalName == "server" && p.GetAttributeValue("address") == server.Host) != null)
                    continue;

                var element = server.ConvertToXElement();
                if (element != null)
                    farm.Add(element);
            }
            return farm;
        }
    }
}
