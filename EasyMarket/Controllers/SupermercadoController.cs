using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class SupermercadoController : Controller
    {
        // GET: Supermercado
        public ActionResult Index()
        {
            return View();
        }

        // GET: Supermercado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Supermercado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supermercado/Create
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

        // GET: Supermercado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Supermercado/Edit/5
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

        // GET: Supermercado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supermercado/Delete/5
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
    }
}
