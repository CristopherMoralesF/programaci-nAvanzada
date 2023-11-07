using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ClassesList
    {
        public int IdClase { get; set; }
        public string? DescripcionClase { get; set; }
        public string? CuentaActivo { get; set; }
        public string? CuentaGasto { get; set; }
        public string? CuentaDepAcumulada { get; set; }
        public int? VidaUtil { get; set; }
    }
}
