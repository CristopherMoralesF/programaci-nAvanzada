using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using assetManagementClassLibrary.Models;

namespace Proyecto_API.Entities
{
    public class ActivoEnt
    {
        public DateTime FechaAdquisicion;

        public int idActivo { get; set; }
        public ClaseEnt claseActivo { get; set; }
        public UbicacionEnt ubicacionActivo { get; set; }
        public UsuariosEnt duennoActivo { get; set; }
        public EstadoEnt estadoActivo { get; set; } 
        public List<ValidacionClaseEnt> validacionesActivo { get; set; }
        public string descripcionActivo { get; set; }
        public double valorAdquisicion { get; set; }
        public DateTime fechaAdquiscion { get; set; }
        public int periodosDepreciados { get; set; }

    }
}