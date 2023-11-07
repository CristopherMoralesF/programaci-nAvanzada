using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ValidacionRiesgoActivo
    {
        public int IdActivo { get; set; }
        public int IdDuenno { get; set; }
        public string? DescripcionActivo { get; set; }
        public string? DescripcionClase { get; set; }
        public int IdClase { get; set; }
        public int? ValidationPercentaje { get; set; }
        public string ValidacionRiesgo { get; set; } = null!;
    }
}
