using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceBIA.Case.SZ.CityManager.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult BaseMap()
        {
            return View();
        }
        public PartialViewResult MapSample()
        {
            return PartialView();
        }
        public PartialViewResult MapSample1()
        {
            return PartialView();
        }
        public PartialViewResult MapSample2()
        {
            return PartialView();
        }
        public PartialViewResult MapSample3()
        {
            return PartialView();
        }
        public PartialViewResult MapSample4()
        {
            return PartialView();
        }

        public PartialViewResult MapSample5()
        {
            return PartialView();
        }
        public PartialViewResult MapSample6()
        {
            return PartialView();
        }
        public PartialViewResult MapSample7()
        {
            return PartialView();
        }
        public PartialViewResult MapSample8()
        {
            return PartialView();
        }
        public PartialViewResult MapSample9()
        {
            return PartialView();
        }

        /// <summary>
        /// 密度图实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult DensityMapSample()
        {
            return PartialView();
        }

        /// <summary>
        /// 热度图实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult HeatMapSample()
        {
            return PartialView();
        }

        /// <summary>
        /// MarkPoint实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult MarkPointSample()
        {
            return PartialView();
        }

        /// <summary>
        /// MarkLine实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult MarkLineSample()
        {
            return PartialView();
        }
        /// <summary>
        /// 迁徙图实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult MigrationSample()
        {
            return PartialView();
        }

        /// <summary>
        /// 密度图结合时间轴实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Timeline_DensityMapSample()
        {
            return PartialView();
        }

        /// <summary>
        /// 热度图结合时间轴实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Timeline_HeatMapSample()
        {
            return PartialView();
        }

        /// <summary>
        /// MarkPoint结合时间轴实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Timeline_MarkPointSample()
        {
            return PartialView();
        }

        /// <summary>
        /// MarkLine结合时间轴实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Timeline_MarkLineSample()
        {
            return PartialView();
        }
        /// <summary>
        /// 迁徙图结合时间轴实例Pview
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Timeline_MigrationSample()
        {
            return PartialView();
        }

        public PartialViewResult KSDensityMapSample()
        {
            return PartialView();
        }
    }
}