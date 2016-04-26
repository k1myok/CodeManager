using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialInsurance;
using SocialInsurance.Models;
using System.Xml.Linq;

namespace SocialInsurance.Controllers
{
    public class SocialInsuranceController : Controller
    {
        //
        public ActionResult Default()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult PersonalInsurance(condition con)
        {
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si010201(con.ID.Trim(), con.IDCard.Trim(), 8, 1);
            XElement xoc = XElement.Parse(temp);
           // List <PersonalInsurance> data = new List<PersonalInsurance>();
            PersonalInsurance p = new PersonalInsurance();
            List<InsuranceDetail> data = new List<InsuranceDetail>();
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count() > 0)
            {
                p.status = "200";
                return View(p);
            }
            else
            {
                var resu = xoc.Descendants("result").ToList();
                var re = xoc.Descendants("row").ToList();
                p.status = "100";
                p.pages = resu[0].Attribute("pages").Value.ToString();
                p.cpage = resu[1].Attribute("cpage").Value.ToString();
                p.rowcount = resu[2].Attribute("rowcount").Value.ToString();
               
                foreach (var item in re)
                {
                    InsuranceDetail Insurance = new InsuranceDetail();
                    Insurance.aab004 = item.Attribute("aab004").Value;
                    Insurance.aac001 = item.Attribute("aac001").Value;
                    Insurance.aac003 = item.Attribute("aac003").Value;
                    Insurance.aae001 = item.Attribute("aae001").Value;
                    Insurance.aif004 = item.Attribute("aif004").Value;
                    Insurance.aif005 = item.Attribute("aif005").Value;
                    data.Add(Insurance);
                }
                
               
               
                //foreach (var item in re)
                //{
                //    p.pages = resu[0].Attribute("pages").Value.ToString();
                //    p.cpage = resu[1].Attribute("cpage").Value.ToString();
                //    p.rowcount = resu[2].Attribute("rowcount").Value.ToString();
                //    p.status = "100";
                //    p.aab004 = item.Attribute("aif005").Value;
                //    p.aac001 = item.Attribute("aac001").Value;
                //    p.aif005 = item.Attribute("aif005").Value;
                //    p.aif004 = item.Attribute("aif004").Value;
                //    p.aae001 = item.Attribute("aae001").Value;
                //    p.aac003 = item.Attribute("aac003").Value;
                //    data.Add(p);
                //}
                
            }
            p.msg = data;
            return View(p);
        }
    }
}
