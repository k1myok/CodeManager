using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialInsurance.Models;
using System.Xml.Linq;

namespace SocialInsurance.Controllers
{
    public class AlipaySocialInsuranceController : Controller
    {
        public PartialViewResult Index()
        {
            return PartialView();
        }
        public PartialViewResult List(string ID,string IDCard)
        {
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si010201(ID, IDCard, 8,1);
            XElement xoc = XElement.Parse(temp);
            Login log = new Login();
            var faultcode = xoc.Descendants("faultcode").ToList();
            var row = xoc.Descendants("row").ToList();
            if (faultcode.Count() > 0)
            {
                log.satus = "200";
                return PartialView(log);
            }
            else 
            {
                log.satus = "100";
                log.Name = row[1].Attribute("aac003").Value;
                log.ID = ID;
                log.IDCard = IDCard;
                return PartialView(log);
            }
        }

        public PartialViewResult Default()
        {
            return PartialView();
        }
        /// <summary>
        /// 企业养老缴费信息查询
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public PartialViewResult PensionInsurance(string ID,string IDCard,long cpage)
        {
            ID = "0500708293";
            IDCard = "441381198208204752";
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si010201(ID,IDCard, 8,cpage);
            XElement xoc = XElement.Parse(temp);
            PensionInsurance p = new PensionInsurance();
            List<PensionInsuranceDetail> data = new List<PensionInsuranceDetail>();
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
                p.cpage = Convert.ToInt32(resu[1].Attribute("cpage").Value);
                p.rowcount =Convert.ToInt32(resu[2].Attribute("rowcount").Value);
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
            return PartialView(p);
        }
        /// <summary>
        /// 个人参保情况查询
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IDCard"></param>
        public PartialViewResult PersonInsuranceStatus(string ID, string IDCard, long cpage)
        {
            ID = "0500708293";
            IDCard = "441381198208204752";
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si120101(ID, IDCard, 8, cpage);
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
                p.pages = resu[0].Attribute("pages").Value.ToString();
                p.cpages = resu[1].Attribute("cpage").Value.ToString();
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
        //
        /// <summary>
        /// 生育缴费查询
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public PartialViewResult BirthInsurance(string ID, string IDCard,long cpage)
        {
            ID = "0500708293";
            IDCard = "441381198208204752";
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si090401(ID, IDCard, 8, cpage);
            XElement xoc = XElement.Parse(temp);
            BirthInsurance birthIn = new BirthInsurance();
            List<BirthInsuranceDetail> detail=new List<BirthInsuranceDetail>();
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count() > 0)
            {
                birthIn.status = "200";
                return PartialView(birthIn);
            }
            else 
            {
                var resut = xoc.Descendants("result").ToList();
                var rows = xoc.Descendants("row").ToList();
                birthIn.status = "100";
                birthIn.pages =Convert.ToInt32(resut[0].Attribute("pages").Value);
                birthIn.cpage = Convert.ToInt32(resut[1].Attribute("cpage").Value);
                birthIn.rowcount=resut[2].Attribute("rowcount").Value.ToString();
               foreach (var item in rows)
	           {
                   BirthInsuranceDetail data= new BirthInsuranceDetail();
                    data.akf088=item.Attribute("akf088").Value;
                    data.aif004=item.Attribute("aif004").Value;
                    data.aif005=item.Attribute("aif005").Value;
                    data.aif006=item.Attribute("aif006").Value;
                    data.akf084=item.Attribute("akf084").Value;
                    data.akf081=item.Attribute("akf081").Value;
                    data.aac001=item.Attribute("aac001").Value;
                    detail.Add(data);
	            }
             
             birthIn.data=detail;
            }
            return PartialView(birthIn);
            //return PartialView();
        }
      
        /// <summary>
        /// 工伤缴费查询
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public PartialViewResult IdustrialInsurance(string ID,string IDCard,long cpage)
        {
             ID = "0500708293";
             IDCard = "441381198208204752";
         var client= new SocialInsuranceService.ShbServClient();
         var temp=client.si070201(ID,IDCard,8,cpage);
         XElement xoc = XElement.Parse(temp);
         IdustrialInsurance Idustrial = new IdustrialInsurance();
         List<IdustrialInsuranceDetail> data = new List<IdustrialInsuranceDetail>();
        var faultcode = xoc.Descendants("faultcode").ToList();
        if (faultcode.Count() > 0)
        {
            Idustrial.status = "200";
            return PartialView(Idustrial);
        }
        else
        {
            var resut = xoc.Descendants("result").ToList();
            var rows = xoc.Descendants("row").ToList();
            Idustrial.status = "100";
            Idustrial.pages =Convert.ToInt32(resut[0].Attribute("pages").Value);
            Idustrial.cpage =Convert.ToInt32( resut[1].Attribute("cpage").Value);
            Idustrial.rowcount = resut[2].Attribute("rowcount").Value.ToString();
            foreach (var item in rows)
            {
                IdustrialInsuranceDetail list = new IdustrialInsuranceDetail();
                list.aac001 = item.Attribute("aac001").Value;
                list.aif004 = item.Attribute("aif004").Value;
                list.aif005 = item.Attribute("aif005").Value;
                list.aif006 = item.Attribute("aif006").Value;
                list.akf081 = item.Attribute("akf081").Value;
                list.akf084 = item.Attribute("akf084").Value;
                list.akf088 = item.Attribute("akf088").Value;
                data.Add(list);
            }
            Idustrial.data = data;
            return PartialView(Idustrial);
        }
        }
        /// <summary>
        ///失业缴费信息查询
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public  PartialViewResult UnemploymentInsurance(string ID,string IDCard,long cpage)
        {
            ID = "0500708293";
            IDCard ="441381198208204752";
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si080201(ID, IDCard, 8, cpage);
            XElement xoc = XElement.Parse(temp);
            UnemploymentInsurance p = new UnemploymentInsurance();
            List<UnemploymentInsuranceDetail> data = new List<UnemploymentInsuranceDetail>();
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count > 0)
            {
                p.status = "200";
                return PartialView(p);
            }
            else
            {
                var rows = xoc.Descendants("row").ToList();
                var result = xoc.Descendants("result").ToList();
                p.status = "100";
                p.pages =Convert.ToInt32(result[0].Attribute("pages").Value);
                p.cpage =Convert.ToInt32( result[1].Attribute("cpage").Value);
                p.rowcount = result[2].Attribute("rowcount").Value.ToString();
                foreach (var item in rows)
                {
                    UnemploymentInsuranceDetail list = new UnemploymentInsuranceDetail();
                    list.aac001 = item.Attribute("aac001").Value;
                    list.aif004 = item.Attribute("aif004").Value;

                    list.aif005 = item.Attribute("aif005").Value;
                    list.aif006 = item.Attribute("aif006").Value;
                    list.akf081 = item.Attribute("akf081").Value;
                    list.akf084 = item.Attribute("akf084").Value;
                    list.akf088 = item.Attribute("akf088").Value;
                    data.Add(list); 
                }
                p.data = data;
                return PartialView(p);
            }
        }

        public PartialViewResult CompanyMonthInsurance(string ID, string IDCard,long cpage)
        {
            ID = "0500708293";
            IDCard = "441381198208204752";
            CompanyMonthInsurance Cm = new CompanyMonthInsurance();
            List<CompanyMonthInsuranceDetail> data = new List<CompanyMonthInsuranceDetail>();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = client.si030601(ID, IDCard,"110",2015,12,cpage);
            XElement xoc = XElement.Parse(temp);
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count > 0)
            {
                Cm.status = "200";
                return PartialView(Cm);
            }
            else
            {
                var rows = xoc.Descendants("row").ToList();
                var result = xoc.Descendants("result").ToList();
                Cm.status = "100";
                Cm.pages = Convert.ToInt32(result[0].Attribute("pages").Value);
                Cm.cpage = Convert.ToInt32(result[1].Attribute("cpage").Value);
                Cm.rowcount = result[2].Attribute("rowcount").Value.ToString();
                foreach (var item in rows)
                {
                    CompanyMonthInsuranceDetail list = new CompanyMonthInsuranceDetail();
                    list.aac001 = item.Attribute("aac001").Value;
                    list.akf088 = item.Attribute("akf088").Value;
                    list.aif034 = item.Attribute("aif034").Value;
                    list.aae082 = item.Attribute("aae082").Value;
                    list.aae180 = item.Attribute("aae180").Value;
                    list.aif004 = item.Attribute("aif004").Value;
                    list.akf084 = item.Attribute("akf084").Value;
                    list.akf081 = item.Attribute("akf081").Value;
                    data.Add(list);
                }
                Cm.data = data;
                return PartialView(Cm);
            }
        }
        




   }
}
  
