using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OracelEF.Models;
using System.Security.Claims;
using System.Net;
using System.Collections.Generic;
//using BigData.TeamTools.DAL;
using System.IO;
using System.Configuration;
using TransformUtility;
using System.Text;
using System.Security;
using OracelEF;


namespace BigData.TeamTools.Controllers
{
    public class CsdsController : Controller
    {
        // GET: Csds
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Collection()
        {
            return View();   
        }
        public ActionResult  Error()
        {
            return View();   
         }
        public ActionResult Success()
        {
            return View();
        }
        public string GetName(string  code)
        {
           
            var name = string.Empty;

            var context = new CSDSEntities();
                var result = context.TB_QYINFO.Where(p => p.ID == code
                    ).FirstOrDefault();
           
            if (result != null)
            {
              name = result.NSR_MC;
            }
            else
            { 
                name = null;
            }
            return name;
        }

        [HttpPost]
        public JsonResult AddLocation(double real_lon, double real_lat, string address, string CName, string  code, string ZYZL, string file)
        {
            var path = System.IO.Path.Combine(ConfigurationManager.AppSettings["FilePath"]);
            path += @"/"+code+"/";
            var tempbase =  new DealImage(file);
            
            var fileName = System.IO.Path.Combine(path, string.Format("{0}.{1}",code, tempbase.ExtensionName));
            var tempresult = tempbase.ExportToFile(fileName);
              try
                {
                    var context = new CSDSEntities();
                    var temp = context.TB_NSR_EDIT.Where(p => p.ID == code).FirstOrDefault();
                    var map = new MapPoint();
                    map.Lat = real_lat;
                    map.Lon = real_lon;
                    var re = CoordinateTransformUtility.GCJ02toWGS84(map);
                    if (temp == null)
                    {
                        //var url=string .Format("http://content.china-ccw.com:6080/arcgis/rest/services/CSDSGT/CSDSGTZT/MapServer/1/query?where=&text=&objectIds=&time=&geometry={0}%2C{1}&geometryType=esriGeometryPoint&inSR=&spatialRel=esriSpatialRelWithin&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&returnIdsOnly=true&returnCountOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&gdbVersion=&returnDistinctValues=false&f=pjson",re.Lat,real_lon);                       
                        //var test = HttpHelper.GetHtmlEx(url,Encoding.UTF8);
                        var model = new  TB_NSR_EDIT();
                        model.NSR_MC = CName;
                        model.ID = code;
                        model.LON =re.Lon;
                        model.LAT =re.Lat;
                        model.FADDRESS = address;
                        model.ZYZL = ZYZL;
                        model.EDITTIME = DateTime.Now;
                        //model.EDITUSRE = HttpContext.User.Identity.Name;
                        model.XYSYSTEM = "WGS84";
                        context.TB_NSR_EDIT.Add(model);
                        var result = context.SaveChanges() > 0;
                        if (result)
                        {
                            return Json(new { State = true }, JsonRequestBehavior.AllowGet);
                        }
                        
                        else
                        {
                            return Json(new { State = false }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    else if (temp != null)
                    {
                        temp.NSR_MC = CName;
                        temp.ID = code;
                        temp.LON = re.Lon;
                        temp.ZYZL = ZYZL;
                        temp.LAT = re.Lat;
                        temp.FADDRESS = address;
                        temp.EDITTIME = DateTime.Now;
                        temp.XYSYSTEM = "WGS84";
                        return Json(new { State = (context.SaveChanges() > 0) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { State = false }, JsonRequestBehavior.AllowGet);
                    }
                  
                }
                catch (Exception ex)
                {
                    //return View();
                    return Json(new { State = false }, JsonRequestBehavior.AllowGet);
                }
        }
        public string CheckIn(double real_lat, double real_lon)
        {
            var map = new MapPoint();
            map.Lat =real_lat;
            map.Lon =real_lon;
            var re = CoordinateTransformUtility.GCJ02toWGS84(map);
            var url = System.IO.Path.Combine(ConfigurationManager.AppSettings["URL"]);
            url += "where=&text=&objectIds=&time=&geometry={0}%2C{1}&geometryType=esriGeometryPoint&inSR=&spatialRel=esriSpatialRelWithin&relationParam=&outFields=*&returnGeometry=true&maxAllowableOffset=&geometryPrecision=&outSR=&returnIdsOnly=true&returnCountOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&returnZ=false&returnM=false&gdbVersion=&returnDistinctValues=false&f=pjson";
             url = string.Format(url,real_lon, re.Lat );
            var result = HttpHelper.GetHtmlEx(url, Encoding.UTF8);
            return result;
           
        }
         [HttpPost]
        public JsonResult UpdateLocation(double real_lon, double real_lat, string address, string CName, string code, string file,string ZYZL)
        {
             if(string.IsNullOrEmpty(file))
             return Json(new { State = false }, JsonRequestBehavior.AllowGet);
             var path = System.IO.Path.Combine(ConfigurationManager.AppSettings["FilePath"]);
            var tempbase = new DealImage(file);
            var fileName = System.IO.Path.Combine(path, string.Format("{0}.{1}", code, tempbase.ExtensionName));
            var tempresult = tempbase.ExportToFile(fileName);
            var context = new CSDSEntities();
            var temp = context.TB_NSR_EDIT.Where(p => p.ID == code).FirstOrDefault();
            var map = new MapPoint();
            map.Lat = real_lat;
            map.Lon = real_lon;
            var re = CoordinateTransformUtility.GCJ02toWGS84(map);
            temp.NSR_MC = CName;
            temp.ID= code;
            temp.LON =re.Lon;
            temp.ZYZL = ZYZL;
            temp.LAT =re.Lat;
            temp.FADDRESS = address;
            temp.EDITTIME = DateTime.Now;
            temp.XYSYSTEM = "WGS84";
            return Json(new { State = (context.SaveChanges() > 0) }, JsonRequestBehavior.AllowGet);
        }
         public ActionResult Introduction()
         {
             return View();
         }
    }
}