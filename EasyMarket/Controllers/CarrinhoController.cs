using EasyMarket.Daos;
using EasyMarket.Models;
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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(FormCollection collection)
        {
            Produto p = ProdutoDao.BuscarPorCodigo(collection["barcode"]);
            return Json(p);
        }

        [HttpPost]
        public ActionResult Abrir()
        {
            return Json(new { Status = 1 });
        }
    }
}