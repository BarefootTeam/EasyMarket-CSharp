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

            if (p != null)
            {
                p.FormatarValor();

                return Json(new {
                    Status = 1,
                    Produto = p
                });
            }

            return Json(new { Status = 0 });
        }

        [HttpPost]
        public ActionResult Adicionar(FormCollection collection)
        {

            Carrinho c = (Carrinho) Session["carrinho"];
            Produto p = ProdutoDao.BuscarPorCodigo(collection["barcode"]);

            if (p != null)
            {
                ItemCarrinho ic = c.BuscarItem(p);

                if(ic != null)
                {
                    ic.Quantidade = ic.Quantidade + 1;
                }
                else
                {
                    ic = new ItemCarrinho();
                    ic.Carrinho = c;
                    ic.Produto = p;
                    ic.Quantidade = 1;
                    ic.Valor = p.PrecoCusto;
                }

                ItemCarrinhoDao.Persistir(ic);

                p.FormatarValor();

                // Reload carrinho com o novo item
                c = CarrinhoDao.BuscarPorId(c.Id, true);
                c.CalcularQuantidade();
                c.CalcularTotal();

                Session["carrinho"] = c;

                return Json(new {
                    Status = 1,
                    Produto = p,
                    Carrinho = c
                });
            }

            return Json(new { Status = 0 });
        }

        [HttpPost]
        public ActionResult Remover(FormCollection collection)
        {

            Carrinho c = (Carrinho)Session["carrinho"];
            Produto p = ProdutoDao.BuscarPorCodigo(collection["barcode"]);

            if (p != null)
            {
                ItemCarrinho ic = c.BuscarItem(p);

                if (ic != null)
                {
                    if(ic.Quantidade > 1)
                    {
                        ic.Quantidade--;
                        ItemCarrinhoDao.Persistir(ic);
                    }
                    else
                    {
                        ItemCarrinhoDao.Excluir(ic);
                    }

                    p.FormatarValor();

                    // Reload carrinho com o novo item
                    c = CarrinhoDao.BuscarPorId(c.Id, true);
                    c.CalcularQuantidade();
                    c.CalcularTotal();

                    Session["carrinho"] = c;

                    return Json(new
                    {
                        Status = 1,
                        Produto = p,
                        Carrinho = c
                    });
                }

            }

            return Json(new { Status = 0 });
        }

        [HttpPost]
        public ActionResult Abrir()
        {
            if (Session["carrinho"] != null)
            {
                return Json(new { Status = 1 });
            }
            else
            {
                Carrinho c = new Carrinho();
                c.Data = DateTime.Today;
                c.Status = true;
                c.Usuario = UsuarioDao.BuscarPorId(1L);

                Carrinho carrinho = CarrinhoDao.PersistirRetorno(c);
                if(carrinho != null)
                {
                    Session["carrinho"] = carrinho;
                    return Json(new { Status = 1 });
                }
                else
                {
                    return Json(new { Status = 0 });
                }

            }
        }

        [HttpPost]
        public ActionResult Finalizar()
        {
            Session["carrinho"] = null;
            return Json(new { Status = 1 });
        }
    }
}