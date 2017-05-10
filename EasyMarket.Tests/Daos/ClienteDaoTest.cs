using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyMarket.Models;
using EasyMarket.Daos;
using System.Collections.Generic;

namespace EasyMarket.Tests.Daos
{
    [TestClass]
    public class ClienteDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Cliente> Clientes  = ClienteDao.BuscarTodos();
            Assert.IsTrue(Clientes.Count > 0);

        }

        [TestMethod]
        public void PersistirInserir()
        {
            Cliente u = new Cliente();
            u.Nome = "LeoOn";
            u.Cpf = "213213";
            Assert.IsTrue(ClienteDao.Persistir(u));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Cliente u = new Cliente();
            u.Id = ClienteDao.getLastId();
            u.Nome = "LeoOn";
            u.Cpf = "012012032112";
            Assert.IsTrue(ClienteDao.Persistir(u));
        }
        [TestMethod]
        public void BuscarID()
        {
            Cliente u = ClienteDao.BuscarPorId(1);
            Assert.IsNotNull(u);
        }

        [TestMethod]
        public void GetLastId()
        {
            Assert.IsNotNull(ClienteDao.getLastId());
        }
        [TestMethod]
        public void Deletar()
        {
            Cliente u = new Cliente();
            u.Id = ClienteDao.getLastId();

            Assert.IsTrue(ClienteDao.Excluir(u));
        }
    }
}
