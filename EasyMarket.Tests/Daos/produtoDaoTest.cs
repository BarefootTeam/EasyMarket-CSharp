using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EasyMarket.Models;
using EasyMarket.Daos;

namespace EasyMarket.Tests.Daos
{
    [TestClass]
    public class ProdutoDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<Produto> Produtos = ProdutoDao.BuscarTodos();
            Assert.IsTrue(Produtos.Count > 0);

        }

        [TestMethod]
        public void BuscarPorSupermercado()
        {
            List<Produto> Produtos = ProdutoDao.BuscarPorSupermercado(1L);
            Assert.IsTrue(Produtos.Count > 0);

        }

        [TestMethod]
        public void PersistirInserir()
        {
                
            Produto p = new Produto();
            p.Supermercado = new Supermercado();
            p.Nome = "Biscoito";
            p.Cod = "B1234";
            p.Descricao = "Biscoito da Marca Aymoré";
            p.PrecoCusto = 1.20M;
            p.Foto = "C:\teste_para_foto\foto_teste.jpg";
            p.Supermercado.Id = 1L;
            Assert.IsTrue(ProdutoDao.Persistir(p));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            Produto p = new Produto();
            p.Supermercado = new Supermercado();
            p.Id = 3L;
            p.Nome = "Cookies";
            p.Cod = "B1234";
            p.Descricao = "Cookies da Marca Aymoré";
            p.PrecoCusto = 1.20M;
            p.Foto = "C:\teste_para_foto\foto_teste2.jpg";
            p.Supermercado.Id = 1L;
            Assert.IsTrue(ProdutoDao.Persistir(p));
        }

        [TestMethod]
        public void BuscarID()
        {
            Produto p = ProdutoDao.BuscarPorId(3);
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void GetLastId()
        {
            Assert.IsNotNull(ProdutoDao.getLastId());
        }


        [TestMethod]
        public void Deletar()
        {
            Produto p = new Produto();
            p.Id = ProdutoDao.getLastId();

            Assert.IsTrue(ProdutoDao.Excluir(p));
        }
    }
}
