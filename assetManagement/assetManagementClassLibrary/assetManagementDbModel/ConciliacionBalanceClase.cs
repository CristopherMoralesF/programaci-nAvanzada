using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ConciliacionBalanceClase
    {
        public int IdClase { get; set; }
        public string IdCuenta { get; set; } = null!;
        public string? CategoriaCuenta { get; set; }
        public string? DescripcionClase { get; set; }
        public double? Balance { get; set; }
        public double? ValorAuxiliar { get; set; }
        public double? Diferencia { get; set; }
    }
}
