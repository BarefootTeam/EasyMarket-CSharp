using EasyMarket.Daos;
using EasyMarket.Models;
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
            return View(SupermercadoDao.BuscarTodos());
        }

        // GET: Supermercado/Details/5
        public ActionResult Details(int id)
        {
            return View(SupermercadoDao.BuscarPorId(id));
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
                Supermercado s = new Supermercado();
                s.Nome = collection["Nome"];
                s.Cnpj = collection["Cnpj"];
                if (!SupermercadoDao.Persistir(s))
                {
                    return View();
                }

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
            return View(SupermercadoDao.BuscarPorId(id));
        }

        // POST: Supermercado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Supermercado s = new Supermercado();
                s.Id = id;
                s.Nome = collection["Nome"];
                s.Cnpj = collection["Cnpj"];
                if (!SupermercadoDao.Persistir(s))
                {
                    return View();
                }
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
            return View(SupermercadoDao.BuscarPorId(id));
        }

        // POST: Supermercado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SupermercadoDao.Excluir(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
