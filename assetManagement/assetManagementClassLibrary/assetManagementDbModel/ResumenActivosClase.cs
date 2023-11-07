using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ResumenActivosClase
    {
        public int IdClase { get; set; }
        public string? DescripcionClase { get; set; }
        public int? TotalActivos { get; set; }
    }
}
