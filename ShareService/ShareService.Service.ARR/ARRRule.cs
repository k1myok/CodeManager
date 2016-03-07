using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShareService.Service.ARR
{
    public class ARRRule
    {
        public string Name { get; set; }

        public string InURL { get; set; }

        public string ToFarm { get; set; }

        public XElement ConvertToXElement()
        {
            var url = new Uri(this.InURL);

            var ruleTemplateFile = System.IO.Path.Combine(ARRConfig.TemplatePath, "Rule.xml");
            var ruleTemplate = System.IO.File.ReadAllText(ruleTemplateFile);

            var xml = string.Format(ruleTemplate, this.Name, url.PathAndQuery.Substring(1), this.ToFarm, ARRConfig.Host, ARRConfig.Port);
            var ruleElement = XElement.Parse(xml);
            return ruleElement;
        }

    }
}
