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
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count() > 0)
            {
                return null;
            }
            else
            {
                PersonalInsurance p = new PersonalInsurance();
                
                var resu = xoc.Descendants("result").ToList();
                p.pages = resu[0].Attribute("pages").Value.ToString();
                p.cpage = resu[1].Attribute("cpage").Value.ToString();
                p.rowcount = resu[2].Attribute("rowcount").Value.ToString();

                // p.pages =

                
               // p.pages =xoc.Element(XName.Get("temp")).Attribute("pages").Value;
                //p.rowcount = xoc.Element(XName.Get("temp")).Attribute("rowcount").Value;
                //p.cpage = xoc.Element(XName.Get("temp")).Attribute("cpage").Value;
                var re = xoc.Descendants("row").ToList();
                List<InsuranceDetail> data = new List<InsuranceDetail>();
                foreach (var item in re)
                {
                    InsuranceDetail detail = new InsuranceDetail();
                    detail.aab004 = item.Attribute("aif005").Value;
                    detail.aac001 = item.Attribute("aac001").Value;
                    detail.aif005 = item.Attribute("aif005").Value;
                    detail.aif004 = item.Attribute("aif004").Value;
                    detail.aae001 = item.Attribute("aae001").Value;
                    detail.aac003 = item.Attribute("aac003").Value;
                    data.Add(detail);
                }
                p.msg.AddRange(data);
                return View(p);
            }
        }
    }
}
