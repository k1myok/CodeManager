using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnMVC.Models;

namespace LearnMVC.Controllers
{
    public class LearnMVCController : Controller
    {
        //
        // GET: /LearnMVC/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /LearnMVC/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LearnMVC/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LearnMVC/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LearnMVC/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /LearnMVC/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LearnMVC/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LearnMVC/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AddAction()
        {


            return View();
        }
        public PartialViewResult Add()
        {

            return PartialView();
        }
    }
}
