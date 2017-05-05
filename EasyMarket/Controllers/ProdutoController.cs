using EasyMarket.Daos;
using EasyMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View(ProdutoDao.BuscarTodos());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View(ProdutoDao.BuscarPorId(id));
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                /*
                Produto p = new Produto();
                p.Supermercado = new Supermercado();
                p.Nome = "Biscoito";
                p.Cod = "B1234";
                p.Descricao = "Biscoito da Marca Aymoré";
                p.PrecoCusto = 1.20M;
                p.Foto = "C:\teste_para_foto\foto_teste.jpg";
                p.Supermercado.Id = 1L;
                Assert.IsTrue(ProdutoDao.Persistir(p));

                if (!ProdutoDao.Persistir(p))
                {
                    return View();
                }
                */
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            return View(ProdutoDao.BuscarPorId(id));
        }

        // POST: Produto/Edit/5
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

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ProdutoDao.BuscarPorId(id));
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ProdutoDao.Excluir(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
