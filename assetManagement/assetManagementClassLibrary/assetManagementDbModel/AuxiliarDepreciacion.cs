using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class AuxiliarDepreciacion
    {
        public int IdActivo { get; set; }
        public string? DescripcionActivo { get; set; }
        public double ValorAdquisicion { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public int? PeriodosDepreciados { get; set; }
        public string? DescripcionClase { get; set; }
        public int? VidaUtil { get; set; }
        public int IdClase { get; set; }
        public double? DepreciacionMensual { get; set; }
        public double? DepreciacionAcumulada { get; set; }
    }
}
