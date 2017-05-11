using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class Compra
    {
        public long Id { get; set; }
        public String Nome { get; set; }
        public decimal Total { get; set; }
        public DateTime data { get; set; }
    }
}