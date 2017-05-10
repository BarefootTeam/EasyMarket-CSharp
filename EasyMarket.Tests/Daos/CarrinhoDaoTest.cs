using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyMarket.Daos;
using System.Collections.Generic;
using EasyMarket.Models;

namespace EasyMarket.Tests.Daos
{
    [TestClass]
    public class CarrinhoDaoTest
    {

        [TestMethod]
        public void BuscarTodos()
        {
            List<Carrinho> Carrinhos = CarrinhoDao.BuscarTodos();
            Assert.IsTrue(Carrinhos.Count > 0);

        }

        [TestMethod]
        public void PersistirInserir()
        {
            Carrinho c = new Carrinho();
            c.Cliente = new Cliente();
            c.Cliente.Id = 2L;
            c.Status = true;
            c.Data = new DateTime(2000,01,01);
            Assert.IsTrue(CarrinhoDao.Persistir(c));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Carrinho c = new Carrinho();
            c.Cliente = new Cliente();
            c.Cliente.Id = 1L;
            c.Id = CarrinhoDao.getLastId();
            c.Status = true;
            c.Data = new DateTime(2050, 01, 01);
            Assert.IsTrue(CarrinhoDao.Persistir(c));
        }

        [TestMethod]
        public void BuscarID()
        {
            Carrinho c = CarrinhoDao.BuscarPorId(1L,true);
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetLastId()
        {
            Assert.IsNotNull(CarrinhoDao.getLastId());
        }


        [TestMethod]
        public void Deletar()
        {
            Carrinho c = new Carrinho();
            c.Id =  CarrinhoDao.getLastId();

            Assert.IsTrue(CarrinhoDao.Excluir(c));
        }

        [TestMethod]
        public void BuscarPorStatus()
        {
            List<Carrinho> Carrinhos = CarrinhoDao.buscarPorStatus(true);
            Assert.IsTrue(Carrinhos.Count > 0);

        }


    }
}
