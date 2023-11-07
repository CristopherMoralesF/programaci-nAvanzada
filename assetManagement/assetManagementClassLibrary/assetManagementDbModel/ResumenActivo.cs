using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ResumenActivo
    {
        public int IdActivo { get; set; }
        public string? DescripcionActivo { get; set; }
        public double ValorAdquisicion { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public string? DescripcionClase { get; set; }
        public int? VidaUtil { get; set; }
        public int? PeriodosDepreciados { get; set; }
        public string IdEdificio { get; set; } = null!;
        public string DescripcionSeccion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string DescripcionEstado { get; set; } = null!;
    }
}
