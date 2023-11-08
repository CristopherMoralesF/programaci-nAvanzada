using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_API.Entities
{
    public class IndicadoresEnt
    {
        public int totalActivos { get; set; }
        public double totalInversion { get; set; }
        public double porcentajeCumplimineto { get; set; }
        public int totalActivosUsuario { get; set; }
        public List<IndicadoresRiesgoEnt> activosUsuario { get; set; }
        public List<IndicadoresRiesgoEnt> activosCompannia { get; set; }   
        
        public List<ClaseEnt> resumenClases { get; set; }

        public List<ClaseEnt> resumenClasesRiesgo { get; set; }

    }
}