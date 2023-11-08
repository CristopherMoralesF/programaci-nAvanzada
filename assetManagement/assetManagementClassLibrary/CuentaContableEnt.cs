using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_API.Entities
{
    public class CuentaContableEnt
    {
        public string idCuenta { get; set; }
        public string descripcionCuenta { get; set; }
        public CategoriaCuentaEnt categoriaCuenta { get; set; }
        public double totalDebitos { get; set; }
        public double totalCreditos { get; set; }   
        public double balance { get; set; }
        public string naturaleza { get; set; }
    }
}