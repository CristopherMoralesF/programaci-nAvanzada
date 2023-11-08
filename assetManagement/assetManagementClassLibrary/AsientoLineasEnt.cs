using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_API.Entities
{
    public class AsientoLineasEnt
    {
        public int idAsientoLinea { get; set; }
        public string idCuentaContable { get; set; }
        public string descripcionLinea { get; set; }    
        public double debito { get; set; }  
        public double credito { get; set; }
    }
}