using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class ItemCarrinho
    {
        public long Id  { get; set; }
        public Carrinho Carrinho { get; set; }
        public Produto Produto { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        public String ValorFormatado()
        {
            return String.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:C}", Valor);
        }

    }
}