using EasyMarket.Daos;
using EasyMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyMarket.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View(UsuarioDao.BuscarTodos());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View(UsuarioDao.BuscarPorId(id));
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Usuario u = new Usuario();
                u.Login = collection["Login"];
                u.Senha = collection["Senha"];
                u.Nome = collection["Nome"];
                u.Cpf = collection["Cpf"];

                if (!UsuarioDao.Persistir(u))
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View(UsuarioDao.BuscarPorId(id));
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Usuario u = new Usuario();
                u.Id = id;
                u.Login = collection["Login"];
                u.Senha = collection["Senha"];
                u.Nome = collection["Nome"];
                u.Cpf = collection["Cpf"];

                if (!UsuarioDao.Persistir(u))
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View(UsuarioDao.BuscarPorId(id));
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                UsuarioDao.Excluir(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
