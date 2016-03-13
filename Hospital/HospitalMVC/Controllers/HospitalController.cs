using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalData;

namespace HospitalMVC.Controllers
{
    public class HospitalController : Controller
    {
        //
        // GET: /Hospital/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult  GetHospitalList()
        {
            Hospital hos= new Hospital();
            ViewBag.List = new List<Hospital>();
            var context = new CityServiceEntities();
            var result = context.Hospitals.OrderBy(p => p.HospName).Select(p => p.HospName);
            return ViewBag(result);
        }
        /// <summary>
        ///专家科室挂号
        /// </summary>
        /// <returns></returns>
        public ActionResult ExRegister()
        {

            return View();
        }
        /// <summary>
        /// 普通科室挂号
        /// </summary>
        /// <returns></returns>
        public ActionResult ComRegister()
        {
            return View();
        }
        /// <summary>
        /// 获取专家科室列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExDepart()
        {


            return View();
        }
        /// <summary>
        /// 获取普通科室列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetComDepart()
        {
            return View();
        }
        /// <summary>
        /// 获取专家科室号源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExRegister()
        {


            return View();
        }
        /// <summary>
        /// 获取普通科室号源
        /// </summary>
        /// <returns></returns>
        //public ActionResult GetExRegister()
        //{
        //    return View();
        //}
    }
}
