using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ValidacionRiesgoClase
    {
        public int IdClase { get; set; }
        public string? DescripcionClase { get; set; }
        public string ValidacionRiesgo { get; set; } = null!;
        public int? TotalActivos { get; set; }
    }
}
