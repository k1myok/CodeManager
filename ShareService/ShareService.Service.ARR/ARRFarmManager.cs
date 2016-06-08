using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShareService.Service.ARR
{

    public class ARRFarmManager
    {
        private string configFile = System.IO.Path.Combine(Environment.SystemDirectory, @"inetsrv\config\applicationHost.config");

        private XDocument document;

        private XElement webFarmsRoot;

        public ARRFarmManager()
        {
            //icacls C:\Windows\system32\inetsrv\config\applicationHost.config  /grant IIS_IUSRS:f

            this.document = System.Xml.Linq.XDocument.Load(configFile);
            this.webFarmsRoot = document.Root.FindElement(p => p.Name.LocalName == "webFarms");
            if (this.webFarmsRoot == null)
                this.webFarmsRoot = this.CreateFarmRoot(document);
        }

        public bool AddFarm(ARRFarm farm)
        {
            var farmElement = this.webFarmsRoot.FindElement(p => p.GetAttributeValue("name") == farm.Name);
            if (farmElement != null)
                return false;

            farmElement = farm.ConvertToXElement();
            this.webFarmsRoot.Add(farmElement);
            return true;
        }

        public bool RemoveFarm(string farmName)
        {
            var farm = this.webFarmsRoot.FindElement(p => p.GetAttributeValue("name") == farmName);
            if (farm == null)
                return false;

            farm.Remove();
            return true;
        }

        public bool UpdateFarm(ARRFarm farm)
        {
            var farmElement = this.webFarmsRoot.FindElement(p => p.GetAttributeValue("name") == farm.Name);
            if (farmElement == null)
                return false;

            farmElement.Remove();

            return true;
        }
        public bool ResetFarms(ARRFarm[] farms)
        {
            var childElements = this.webFarmsRoot.Elements().ToList();
            for (int index = 0; index < childElements.Count(); index++)
            {
                var element = childElements[index];
                if(element.Name.LocalName == "webFarm")
                {
                    element.Remove();
                }
            }

            farms.ToList().ForEach(p => this.AddFarm(p));
            return true;
        }

        public bool AddServer(string farmName, ARRServer server)
        {
            if (string.IsNullOrEmpty(farmName) || server == null)
                return false;

            var farm = this.webFarmsRoot.FindElement(p => p.GetAttributeValue("name") == farmName);
            if (farm == null)
                return false;

            var element = server.ConvertToXElement();
            if (element == null)
                return false;

            farm.Add(element);
            return true;
        }

        public bool RemoveServer(string farmName, string host)
        {
            var farm = this.webFarmsRoot.FindElement(p => p.GetAttributeValue("name") == farmName);
            if (farm == null)
                return false;

            var targetServer = farm.FindElement(p => p.GetAttributeValue("address") == host);
            if (targetServer == null)
                return false;

            targetServer.Remove();
            return true;
        }

        public bool AddRule(ARRRule rule)
        {
            var farm = document.Root.FindElement("webFarms").FindElement(p => p.Name.LocalName == "webFarm" && p.GetAttributeValue("name") == rule.ToFarm);
            if (farm == null)
                return false;

            var ruleElement = rule.ConvertToXElement();

            var root = document.Root.FindElement("system.webServer/rewrite/globalRules");
            if (root.FindElement(p => p.Name.LocalName == "rule" && p.GetAttributeValue("name") == rule.Name) != null)
                return false;

            root.Add(ruleElement);
            return true;
        }

        public bool UpdateRule(ARRRule rule)
        {
            var farm = document.Root.FindElement("webFarms").FindElement(p => p.Name.LocalName == "webFarm" && p.GetAttributeValue("name") == rule.ToFarm);
            if (farm == null)
                return false;

            var ruleElement = rule.ConvertToXElement();

            var root = document.Root.FindElement("system.webServer/rewrite/globalRules");
            var existedRule = root.FindElement(p => p.Name.LocalName == "rule" && p.GetAttributeValue("name") == rule.Name);
            if (existedRule == null)
                return false;

            existedRule.Remove();
            root.Add(ruleElement);
            return true;
        }

        public bool ResetRules(ARRRule[] rules)
        {
            var root = document.Root.FindElement("system.webServer/rewrite/globalRules");
            if (root == null)
                return false;
            root.RemoveNodes();
            rules.ToList().ForEach(p => AddRule(p));
            return true;
        }

        public bool RemoveRule(string ruleName)
        {
            var root = document.Root.FindElement("system.webServer/rewrite/globalRules");
            var rule = root.FindElement(p => p.Name.LocalName == "rule" && p.GetAttributeValue("name") == ruleName);
            if (rule == null)
                return false;

            rule.Remove();
            return true;
        }

        public bool Save()
        {
            this.document.Save(configFile);
            return true;
        }

        private XElement CreateFarmRoot(XDocument document)
        {
            var template = System.IO.Path.Combine(ARRConfig.TemplatePath, "FarmRoot.xml");
            var farmRoot = XDocument.Load(template);
            document.Root.Add(farmRoot.Root);
            return farmRoot.Root;
        }
    }
}
