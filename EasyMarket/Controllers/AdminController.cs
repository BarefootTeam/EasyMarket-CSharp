using EasyMarket.Daos;
using EasyMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return View();
            }else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: /Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: /Admin/Logout
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index");
        }

        // POST: /Admin/Autenticar
        [HttpPost]
        public ActionResult Autenticar(FormCollection collection)
        {

            String login = collection["login"];
            String senha = collection["senha"];

            Usuario user = UsuarioDao.BuscarPorLogin(login);

            if(user != null)
            {
                if(user.Senha == senha)
                {
                    // Login Correto
                    Session["user"] = user;
                    return RedirectToAction("Index");
                }
                else
                {
                    // Senha Incorreta
                    ViewBag.StatusMessage = "A senha digitada está incorreta";
                }
            }
            else
            {
                // Usuario não encontrado
                ViewBag.StatusMessage = "Usuario não encontrado";
            }

            return View();

        }
        
    }
}