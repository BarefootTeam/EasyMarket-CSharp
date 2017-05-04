using EasyMarket.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Carrinho_Compras()
        {
            return View(CarrinhoDao.buscarPorStatus(true));
        }

        // GET: Carrinho/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Carrinho/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrinho/Create
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

        // GET: Carrinho/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Carrinho/Edit/5
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

        // GET: Carrinho/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Carrinho/Delete/5
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
