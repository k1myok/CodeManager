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
       /// <summary>
       /// 企业养老缴费信息查询
       /// </summary>
       /// <param name="con"></param>
       /// <returns></returns>
        [HttpPost]
        public ActionResult PersonalInsurance(condition con)
        {
           var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si010201(con.ID.Trim(), con.IDCard.Trim(), 8, 1);
            XElement xoc = XElement.Parse(temp);
            PensionInsurance p = new PensionInsurance();
            List<PensionInsuranceDetail> data = new List<PensionInsuranceDetail>();
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
                p.pages = Convert.ToInt32(resu[0].Attribute("pages").Value);
                p.cpage =Convert.ToInt32(resu[1].Attribute("cpage").Value);
                p.rowcount =resu[2].Attribute("rowcount").Value.ToString();
                foreach (var item in re)
                {
                    PensionInsuranceDetail Insurance = new PensionInsuranceDetail();
                    Insurance.aab004 = item.Attribute("aab004").Value;
                    Insurance.aac001 = item.Attribute("aac001").Value;
                    Insurance.aac003 = item.Attribute("aac003").Value;
                    Insurance.aae001 = item.Attribute("aae001").Value;
                    Insurance.aif004 = item.Attribute("aif004").Value;
                    Insurance.aif005 = item.Attribute("aif005").Value;
                    data.Add(Insurance);
                } 
            }
            p.msg = data;
            return View(p);
        }
        /// <summary>
        /// 个人参保情况查询
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IDCard"></param>
        [HttpPost]
        public  PartialViewResult PersonInsuranceStatus(string ID,string IDCard)
        {
            ID = "0500708293";
            IDCard ="441381198208204752";
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si120101(ID, IDCard, 8, 1);
            XElement xoc = XElement.Parse(temp);
            PInsuranceStatu p = new PInsuranceStatu();
            List<PInsuranceStatuDetail> data = new List<PInsuranceStatuDetail>();
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count() > 0)
            {
                p.status = "200";
                return PartialView(p);
            }
            else
            {
                var resu = xoc.Descendants("result").ToList();
                var re = xoc.Descendants("row").ToList();
                p.status = "100";
                p.pages =Convert.ToInt32(resu[0].Attribute("pages").Value);
                p.cpages = Convert.ToInt32(resu[1].Attribute("cpage").Value);
                p.rowcount = resu[2].Attribute("rowcount").Value.ToString();
                foreach (var item in re)
                {
                    PInsuranceStatuDetail Insurance = new PInsuranceStatuDetail();
                    Insurance.aab001 = item.Attribute("aab001").Value;
                    Insurance.aab002 = item.Attribute("aab002").Value;
                    Insurance.aab004 = item.Attribute("aab004").Value;
                    Insurance.aac001 = item.Attribute("aac001").Value;
                    Insurance.aac008 = item.Attribute("aac008").Value;
                    Insurance.aac031 = item.Attribute("aac031").Value;
                    Insurance.akf066 = item.Attribute("akf066").Value;
                    Insurance.akf067 = item.Attribute("akf067").Value;
                    data.Add(Insurance);
                }
            }
            p.data = data;
            return PartialView(p);
        }
    }
}
