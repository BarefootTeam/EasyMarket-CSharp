using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class Carrinho
    {
        public long Id { get; set; }
        public Boolean Status { get; set; }
        public DateTime Data { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemCarrinho> Itens { get; set; }
        public String Total { get; set; }
        public int Quantidade { get; set; }
        
        public void CalcularTotal()
        {
            decimal total = 0;
            foreach(ItemCarrinho i in Itens)
            {
                total += (i.Valor * i.Quantidade);
            }
            Total = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", total);
        }

        public void CalcularQuantidade()
        {
            int total = 0;
            foreach (ItemCarrinho i in Itens)
            {
                total += i.Quantidade;
            }
            Quantidade = total;
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