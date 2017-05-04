using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Convert.ToBoolean(Session["logado"]))
            {
                return View();
            }else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["logado"] = false;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Autenticar(FormCollection collection)
        {
            if(collection["email"] == "celio-rodrigues21@hotmail.com")
            {
                Session["logado"] = true;
                return RedirectToAction("Index");
            }else
            {
                return View();
            }
        }
        
    }
}