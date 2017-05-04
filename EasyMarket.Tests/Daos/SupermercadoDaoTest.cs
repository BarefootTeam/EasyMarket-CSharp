using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyMarket.Models;
using EasyMarket.Daos;
using System.Collections.Generic;

namespace EasyMarket.Tests.Daos
{
    [TestClass]
    public class SupermercadoDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Supermercado> Supermercados = SupermercadoDao.BuscarTodos();
            Assert.IsTrue(Supermercados.Count > 0);
        }

        [TestMethod]
        public void PersistirInserir()
        {
            Supermercado s = new Supermercado();
            s.Nome = "Bahamas";
            s.Cnpj = "012012032112";
            Assert.IsTrue(SupermercadoDao.Persistir(s));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Supermercado s = new Supermercado();
            s.Id = 1L;
            s.Nome = "Makro";
            s.Cnpj = "012012032112";
            Assert.IsTrue(SupermercadoDao.Persistir(s));
        }

        [TestMethod]
        public void BuscarID()
        {
            Supermercado s = SupermercadoDao.BuscarPorId(1);
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void GetLastId()
        {
            Assert.IsNotNull(SupermercadoDao.getLastId());
        }


        [TestMethod]
        public void Deletar()
        {
            Supermercado s = new Supermercado();
            s.Id = SupermercadoDao.getLastId();

            Assert.IsTrue(SupermercadoDao.Excluir(s));
        }
    }
}
