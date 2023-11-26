using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assetManagementClassLibrary
{
    public class IndicadoresRiesgoEnt
    {
        public int idActivo { get; set; }
        public int idDuenno { get; set; }
        public string descripcionActivo { get; set; }
        public double porcentajeValidacion { get; set; }
        public string validacionRiesgo { get; set; }
        public string descripcionClase { get; set; }    
    }
}