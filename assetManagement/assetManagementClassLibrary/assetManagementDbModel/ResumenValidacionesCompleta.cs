using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ResumenValidacionesCompleta
    {
        public int? IdActivo { get; set; }
        public int? IdClase { get; set; }
        public string? DescripcionActivo { get; set; }
        public int IdTipoValidacion { get; set; }
        public string DescripcionValidacion { get; set; } = null!;
        public string? Valor { get; set; }
    }
}
