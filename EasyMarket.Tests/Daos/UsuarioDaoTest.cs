using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyMarket.Models;
using System.Collections.Generic;
using EasyMarket.Daos;

namespace EasyMarket.Tests.Daos
{
    [TestClass]
    public class UsuarioDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Usuario> Usuarios = UsuarioDao.BuscarTodos();
            Assert.IsTrue(Usuarios.Count > 0);

        }

        [TestMethod]
        public void PersistirInserir()
        {
            Usuario u = new Usuario();
            u.Supermercado = new Supermercado();
            u.Supermercado.Id = 1L;
            u.Login = "LeoLogin";
            u.Senha = "LeoSenha";
            u.Nome = "LeoOn";
            u.Cpf = "012012032112";   


            Assert.IsTrue(UsuarioDao.Persistir(u));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Usuario u = new Usuario();
            u.Supermercado = new Supermercado();
            u.Supermercado.Id = 1L;
            u.Id = UsuarioDao.getLastId();
            u.Login = "LeoLast";
            u.Senha = "LeoSenha";
            u.Nome = "LeoOn";
            u.Cpf = "012012032112";


            Assert.IsTrue(UsuarioDao.Persistir(u));
        }

        [TestMethod]
        public void BuscarID()
        {
            Usuario u = UsuarioDao.BuscarPorId(1);
            Assert.IsNotNull(u);
        }

        [TestMethod]
        public void GetLastId()
        {
            Assert.IsNotNull(UsuarioDao.getLastId());
        }


        [TestMethod]
        public void Deletar()
        {
            Usuario u = new Usuario();
            u.Id = UsuarioDao.getLastId();

            Assert.IsTrue(UsuarioDao.Excluir(u));
        }

     }
}
