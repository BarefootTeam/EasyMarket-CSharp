using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class Usuario
    { 
        public long Id { get; set; }
        public String Login { get; set; }
        public String Senha { get; set; }
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public Supermercado Supermercado { get; set; }


    }
}