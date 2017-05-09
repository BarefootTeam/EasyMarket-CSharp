using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class Carrinho
    {
        public long Id { get; set; }
        public Boolean Status { get; set; }
        public DateTime Data { get; set; }
        public Usuario Usuario { get; set; }
        public List<ItemCarrinho> Itens { get; set; }
        
        public decimal Total()
        {
            decimal total = 0;
            foreach(ItemCarrinho i in Itens)
            {
                total += i.Valor;
            }
            return total;
        }

        public int Quantidade()
        {
            int total = 0;
            foreach (ItemCarrinho i in Itens)
            {
                total += i.Quantidade;
            }
            return total;
        }

        public ItemCarrinho BuscarItem(Produto p)
        {

            if (Itens != null && Itens.Count > 0)
            {
                foreach (ItemCarrinho i in Itens)
                {
                    if (i.Produto.Id == p.Id)
                    {
                        return i;
                    }
                }
            }

            return null;
        }
    }
}