using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareService.Service.ARR;

namespace ShareService.ServiceManager.Tests.ARR
{
    [TestClass]
    public class ARRFarmManagerTest
    {
        [TestMethod]
        public void TestAddFarm()
        {
            var target = new ARRFarmManager();
            var farmName = DateTime.Now.ToString("yyyyMMddhhmmss");
            var result = target.AddFarm(new ARRFarm() { Name = farmName });
            Assert.IsTrue(result);
            Assert.IsTrue(target.Save());
            if(result)
            {
                Assert.IsFalse(target.AddFarm(new ARRFarm() { Name = farmName }));
                Assert.IsTrue(target.Save());
            }
        }

        [TestMethod]
        public void TestRemoveFarm()
        {
            var target = new ARRFarmManager();
            var farmName = "20160223101910";
            var result = target.RemoveFarm(farmName);
            Assert.IsTrue(result);
            Assert.IsTrue(target.Save());
            if (result)
            {
                Assert.IsFalse(target.RemoveFarm(DateTime.Now.ToString("yyyyMMddhhmmss")));
                Assert.IsTrue(target.Save());
            }
        }

        [TestMethod]
        public void TestAddServer()
        {
            var target = new ARRFarmManager();
            var farmName = "20160223115806";
            var arrServer = new ARRServer("localhost",801);
            var result = target.AddServer(farmName, arrServer);
            Assert.IsTrue(result);
            Assert.IsTrue(target.Save());
            if (result)
            {
                Assert.IsFalse(target.AddServer(farmName, null));
                Assert.IsTrue(target.Save());
            }
        }

        [TestMethod]
        public void TestAddRule()
        {
            var target = new ARRFarmManager();
            var farmName = "20160223115806";
            var inURL = "http://content.china-ccw.com:6080/arcgis/rest/services/Finance";
            var ruleName = "Finance1";

            var rule = new ARRRule() {
                Name = ruleName,
                InURL = inURL,
                ToFarm = farmName
            };
            var result = target.AddRule(rule);
            Assert.IsTrue(result);
            Assert.IsTrue(target.Save());
            if (result)
            {
                Assert.IsFalse(target.AddRule(rule));
                Assert.IsTrue(target.Save());
                rule.ToFarm = DateTime.Now.ToString("yyyyMMddhhmmss");
                Assert.IsFalse(target.AddRule(rule));
                Assert.IsTrue(target.Save());
                rule.Name = DateTime.Now.ToString("yyyyMMddhhmmss");
                rule.InURL = "url";
                Assert.IsFalse(target.AddRule(rule));
            }
        }

        [TestMethod]
        public void TestRemoveRule()
        {
            var target = new ARRFarmManager();
            var ruleName = "Finance";
            Assert.IsTrue(target.RemoveRule(ruleName));
            Assert.IsTrue(target.Save());
            Assert.IsFalse(target.RemoveRule(ruleName));
            Assert.IsTrue(target.Save());
        }

        [TestMethod]
        public void TestUpdateRule()
        {
            var target = new ARRFarmManager();
            var farmName = "20160223115806";
            var inURL = "http://content.china-ccw.com:6080/arcgis/rest/services/TOCC";
            var ruleName = "Finance";

            var rule = new ARRRule()
            {
                Name = ruleName,
                InURL = inURL,
                ToFarm = farmName
            };
            var result = target.UpdateRule(rule);
            Assert.IsTrue(result);
            Assert.IsTrue(target.Save());
            if (result)
            {
                rule.ToFarm = DateTime.Now.ToString("yyyyMMddhhmmss");
                Assert.IsFalse(target.UpdateRule(rule));
                Assert.IsTrue(target.Save());
                rule.Name = DateTime.Now.ToString("yyyyMMddhhmmss");
                Assert.IsFalse(target.UpdateRule(rule));
                Assert.IsTrue(target.Save());
            }
        }
        [TestMethod]
        public void TestUpdateRules()
        {
            var target = new ARRFarmManager();
            var farmName = "7b38bd54-8344-47ad-a72c-c6c410f85de4";
            var rules = new ARRRule[] {
                new ARRRule()
                {
                    Name = "Finance",
                    InURL = "http://content.china-ccw.com:6080/arcgis/rest/services/Finance",
                    ToFarm = farmName
                },
                new ARRRule()
                {
                    Name = "TOCC",
                    InURL = "http://content.china-ccw.com:6080/arcgis/rest/services/TOCC",
                    ToFarm = farmName
                },
                new ARRRule()
                {
                    Name = "GardenGIS",
                    InURL = "http://content.china-ccw.com:6080/arcgis/rest/services/GardenGIS",
                    ToFarm = farmName
                }
            };
            var result = target.ResetRules(rules);
            Assert.IsTrue(result);
            Assert.IsTrue(target.Save());
        }
    }
}
