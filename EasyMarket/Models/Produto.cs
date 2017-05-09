using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public String Nome { get; set; }
        public String Cod { get; set; }
        public String Descricao { get; set; }
        public decimal PrecoCusto { get; set; }
        public String Foto { get; set; }
        public String Formatado { get; set; }
        public Supermercado Supermercado { get; set; }

        public void FormatarValor()
        {
            Formatado = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", PrecoCusto);
        }

    }
}