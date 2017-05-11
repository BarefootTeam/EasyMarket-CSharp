using EasyMarket.Daos;
using EasyMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if (collection["Convidado"]  != null)
            {
                Cliente convidado = new Cliente();
                convidado = ClienteDao.BuscarPorId(1L);
                Session["cliente"] = convidado;
                return RedirectToAction("Index", "Carrinho");
            }

            Cliente c = ClienteDao.BuscarPorCpf(collection["Cpf"]);

            if(c != null)
            {
                Session["cliente"] = c;
                return RedirectToAction("Index", "Carrinho");
            }
            else
            {
                c = new Cliente();
                c.Nome = collection["Nome"];
                c.Cpf = collection["Cpf"];

		            c = ClienteDao.PersistirRetorno(c);

		        if(c != null) {
			        Session["cliente"] = c;
	                return RedirectToAction("Index", "Carrinho");

		            }else{
	                return RedirectToAction("Index");
		            }
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Pagina de descrição da sua aplicação";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}