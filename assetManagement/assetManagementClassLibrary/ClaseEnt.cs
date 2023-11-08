using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_API.Entities
{
    public class ClaseEnt
    {
        public int idClase { get; set; }
        public string descripcionClase { get; set; }
        public int vidaUtil { get; set; } 
        public CuentaContableEnt cuentaActivo { get; set; }
        public CuentaContableEnt cuentaGasto { get; set; }
        public CuentaContableEnt cuentaDepAcumulada { get; set; }
        public AsientoEnt asientoDepreciacion { get; set; }

        public ValidacionClaseEnt validacionClase { get; set; }

        public List<ValidacionClaseEnt> listaValidaciones { get; set; }

        public int totalActivos { get; set; }   

        public string categorizacionRiesgo { get; set; }    
    }
}