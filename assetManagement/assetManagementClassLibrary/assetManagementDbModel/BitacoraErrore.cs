using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class BitacoraErrore
    {
        public int IdError { get; set; }
        public string Pantalla { get; set; } = null!;
        public string Error { get; set; } = null!;
        public DateTime? Fecha { get; set; }
    }
}
