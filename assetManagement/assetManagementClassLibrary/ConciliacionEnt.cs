using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_API.Entities
{
    public class ConciliacionEnt
    {

        public int idClase { get; set; }
        public string idCuenta { get; set; }
        public string categoriaCuenta { get; set; }  
        public string descripcionClase { get; set; }    
        public double balance { get; set; }
        public double valorAuxiliar { get; set; }
        public double diferencia { get; set; }


    }
}