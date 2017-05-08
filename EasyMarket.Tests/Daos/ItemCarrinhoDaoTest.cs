using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EasyMarket.Models;
using EasyMarket.Daos;

namespace EasyMarket.Tests.Daos
{
    [TestClass]
    public class ItemCarrinhoDaoTest
    {
        [TestMethod]
        public void BuscarTodos()
        {
            List<ItemCarrinho> Itens = ItemCarrinhoDao.BuscarTodos();
            Assert.IsTrue(Itens.Count > 0);

        }

        [TestMethod]
        public void BuscarPorProduto()
        {
            List<ItemCarrinho> Itens = ItemCarrinhoDao.BuscarPorProduto(1L);
            Assert.IsTrue(Itens.Count > 0);

        }

        [TestMethod]
        public void BuscarPorCarrinho()
        {
            List<ItemCarrinho> Itens = ItemCarrinhoDao.BuscarPorCarrinho(1L);
            Assert.IsTrue(Itens.Count > 0);

        }

        [TestMethod]
        public void PersistirInserir()
        {
            ItemCarrinho itens = new ItemCarrinho();
            itens.Carrinho = new Carrinho();
            itens.Carrinho.Id = 1L;
            itens.Produto = new Produto();
            itens.Produto.Id = 2L;
            itens.Valor = 2.20M;
            itens.Quantidade = 3;
            Assert.IsTrue(ItemCarrinhoDao.Persistir(itens));
        }

        [TestMethod]
        public void PersistirAtualizar()
        {
            ItemCarrinho itens = new ItemCarrinho();
            itens.Id = 2L;
            itens.Carrinho = new Carrinho();
            itens.Carrinho.Id = 1L;
            itens.Produto = new Produto();
            itens.Produto.Id = 7L;
            itens.Valor = 5.20M;
            itens.Quantidade = 12;
            Assert.IsTrue(ItemCarrinhoDao.Persistir(itens));
        }

        [TestMethod]
        public void BuscarId()
        {
            ItemCarrinho item = ItemCarrinhoDao.BuscarPorId(1);
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void GetLastId()
        {
            Assert.IsNotNull(ItemCarrinhoDao.getLastId());
        }


        [TestMethod]
        public void Deletar()
        {
            ItemCarrinho itens = new ItemCarrinho();
            itens.Id = ItemCarrinhoDao.getLastId();

            Assert.IsTrue(ItemCarrinhoDao.Excluir(itens));
        }

        
    }
}
