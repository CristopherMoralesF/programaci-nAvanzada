using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ResumenAsiento
    {
        public int IdAsiento { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = null!;
        public string? DescripcionClase { get; set; }
        public double? TotalAsiento { get; set; }
    }
}
