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
        public PartialViewResult QueryResult(string type)
        {
            ViewBag.Type = type;
            Login log = new Login();
             log=Session["userinfo"] as Login;

            return PartialView(log);
        }
        public PartialViewResult Index()
        {
            return PartialView();
        }
        public PartialViewResult List(string ID,string IDCard)
        {
            Login log = new Login();
            var client = new SocialInsuranceService.ShbServClient();
            var temp=string.Empty;
            try
            {
               temp = client.si010201(ID, IDCard, 8, 1);
            }
            catch (Exception ex)
            {
                log.satus = "300";
                return PartialView(log);
            }
            //var temp = client.si010201(ID, IDCard, 8,1);
            XElement xoc = XElement.Parse(temp);
            var faultcode = xoc.Descendants("faultcode").ToList();
            var row = xoc.Descendants("row").ToList();
            if (faultcode.Count() > 0)
            {
                log.satus = "200";
                return PartialView(log);
            }
            else 
            {
                Session["userinfo"] = log;
                Session["ID"] = ID;
                Session["IDCard"] = IDCard;
                log.satus = "100";
                log.Name = row[1].Attribute("aac003").Value;
                List<int> year = new List<int>();
                foreach (var item in row)
                {
                  var tem=Convert.ToInt32(item.Attribute("aae001").Value);
                    year.Add(tem);
                }
                log.year = year;
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
        public PartialViewResult PensionInsurance(long cpage)
        {
            var ID = Session["ID"].ToString();
            var IDCard = Session["IDCard"].ToString();
            PensionInsurance p = new PensionInsurance();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = string.Empty;
            try
            {
                temp = client.si010201(ID, IDCard, 8, cpage);
            }
            catch (Exception ex)
            {
                p.status = "300";
                return PartialView(p);
            }
            XElement xoc = XElement.Parse(temp);
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
                p.rowcount = resu[2].Attribute("rowcount").Value.ToString();
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
        public PartialViewResult PersonInsuranceStatus(long cpage)
        {
            var ID = Session["ID"].ToString();
            var IDCard = Session["IDCard"].ToString();
            PInsuranceStatu p = new PInsuranceStatu();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = string.Empty;
            try
            {
                temp = client.si120101(ID, IDCard, 8, cpage);
            }
            catch (Exception ex)
            {
                 p.status = "300";
                return PartialView(p);
            }
            XElement xoc = XElement.Parse(temp);
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
                p.cpages =Convert.ToInt32(resu[1].Attribute("cpage").Value);
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
        public PartialViewResult BirthInsurance(long cpage)
        {
            var ID = Session["ID"].ToString();
            var IDCard = Session["IDCard"].ToString();
            BirthInsurance birthIn = new BirthInsurance();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = string.Empty;
            try
            {
                temp = client.si090401(ID, IDCard, 8, cpage);
            }
            catch (Exception ex)
            {
                birthIn.status = "300";
                return PartialView(birthIn);
            }
        
            XElement xoc = XElement.Parse(temp);
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
        public PartialViewResult IdustrialInsurance(long cpage)
        {
          var ID = Session["ID"].ToString();
         var IDCard = Session["IDCard"].ToString();
         IdustrialInsurance Idustrial = new IdustrialInsurance();
         var client= new SocialInsuranceService.ShbServClient();
         var temp = string.Empty;
         try
         {
             temp = client.si070201(ID, IDCard, 8, cpage);
         }
         catch (Exception ex)
         {
             Idustrial.status = "300";
             return PartialView(Idustrial);
         }
        
         XElement xoc = XElement.Parse(temp);
         
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
        public  PartialViewResult UnemploymentInsurance(long cpage)
        {
            var ID = Session["ID"].ToString();
            var IDCard = Session["IDCard"].ToString();
            UnemploymentInsurance p = new UnemploymentInsurance();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = string.Empty;
            try
            {
                temp = client.si080201(ID, IDCard, 8, cpage);
            }
            catch (Exception)
            {
                p.status = "300";
                return PartialView(p);
            }
            
            XElement xoc = XElement.Parse(temp);
           
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
       /// <summary>
       /// 企业养老缴费信息月度查询
       /// </summary>
       /// <param name="ID"></param>
       /// <param name="IDCard"></param>
       /// <param name="cpage"></param>
       /// <returns></returns>
        public PartialViewResult CompanyMonthInsurance(long cpage,long year)
        {
            var ID = Session["ID"].ToString();
            var IDCard = Session["IDCard"].ToString();
            CompanyMonthInsurance Cm = new CompanyMonthInsurance();
            List<CompanyMonthInsuranceDetail> data = new List<CompanyMonthInsuranceDetail>();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = string.Empty;
            try
            {
              temp = client.si030601(ID, IDCard, "110", year, 12, 1);
            }
            catch (Exception ex)
            {
                Cm.status = "300";
                return PartialView(Cm);
               
            }
           
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
                Cm.cpage = Convert.ToInt32(cpage);
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
        /// <summary>
        /// 机关养老缴费信息查询
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IDCard"></param>
        /// <param name="cpage"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public PartialViewResult OfficeMonthInsurance(long cpage, long year)
        {
             var ID = Session["ID"].ToString();
            var IDCard = Session["IDCard"].ToString();
            OfficeMonthInsurance OffIn = new OfficeMonthInsurance();
            List<OfficeMonthInsuranceDetail> data = new List<OfficeMonthInsuranceDetail>();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = string.Empty;
            try
            {
              temp = client.si030602(ID, IDCard, "110", year, 20, 1);
            }
            catch (Exception ex)
            {
                OffIn.status = "300";
                return PartialView(OffIn); 
            }
            
          
            XElement xoc = XElement.Parse(temp);
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count > 0)
            {
             OffIn.status = "200";
               return PartialView(OffIn);
            }
            else
            {
                var rows = xoc.Descendants("row").ToList();
                var result = xoc.Descendants("result").ToList();
                OffIn.status = "100";
                OffIn.pages = Convert.ToInt32(result[0].Attribute("pages").Value);
                OffIn.cpage = Convert.ToInt32(cpage);
                OffIn.rowcount = result[2].Attribute("rowcount").Value.ToString();
                foreach (var item in rows)
                {
                    OfficeMonthInsuranceDetail list = new OfficeMonthInsuranceDetail();
                    list.aac001 = item.Attribute("aac001").Value;
                    list.akf088 = item.Attribute("akf088").Value;
                    list.aae082 = item.Attribute("aae082").Value;
                    list.aae180 = item.Attribute("aae180").Value;
                    list.aif004 = item.Attribute("aif004").Value;
                    list.akf084 = item.Attribute("akf084").Value;
                    list.akf081 = item.Attribute("akf081").Value;
                    data.Add(list);
                }
                OffIn.data = data;
                return PartialView(OffIn);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IDCard"></param>
        /// <param name="cpage"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public PartialViewResult EmployeehealthInsurance(long cpage, long year)
        {
             var ID = Session["ID"].ToString();
            var IDCard = Session["IDCard"].ToString();
            EmployeehealthInsurance OffIn = new EmployeehealthInsurance();
            List<EmployeehealthInsuranceDetail> data = new List<EmployeehealthInsuranceDetail>();
            var client = new SocialInsuranceService.ShbServClient();
            var temp = string.Empty;
            try
            {
             temp = client.si040401(ID, IDCard, "110", year, 20, 1);
            }
            catch (Exception ex)
            {
                 OffIn.status = "300";
                return PartialView(OffIn);
            }
            XElement xoc = XElement.Parse(temp);
            var faultcode = xoc.Descendants("faultcode").ToList();
            if (faultcode.Count > 0)
            {
                OffIn.status = "200";
                return PartialView(OffIn);
            }
            else
            {
                var rows = xoc.Descendants("row").ToList();
                var result = xoc.Descendants("result").ToList();
                OffIn.status = "100";
                OffIn.pages = Convert.ToInt32(result[0].Attribute("pages").Value);
                OffIn.cpage = Convert.ToInt32(cpage);
                OffIn.rowcount = result[2].Attribute("rowcount").Value.ToString();
                foreach (var item in rows)
                {
                    EmployeehealthInsuranceDetail list = new EmployeehealthInsuranceDetail();
                    list.aac001 = item.Attribute("aac001").Value;
                    list.akf088 = item.Attribute("akf088").Value;
                    list.aae180 = item.Attribute("aae180").Value;
                    list.aae082 = item.Attribute("aae082").Value;
                    list.aae080 = item.Attribute("aae080").Value;
                    list.aif004 = item.Attribute("aif004").Value;
                    list.aif057 = item.Attribute("aif057").Value;
                    list.akf084 = item.Attribute("akf084").Value;
                    list.akf081 = item.Attribute("akf081").Value;
                    data.Add(list);
                }
                OffIn.data = data;
                return PartialView(OffIn);
            }
        }

   }
}
  
