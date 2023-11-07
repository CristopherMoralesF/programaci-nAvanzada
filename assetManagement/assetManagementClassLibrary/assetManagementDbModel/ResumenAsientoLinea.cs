using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ResumenAsientoLinea
    {
        public int IdAsientoLinea { get; set; }
        public string IdCuentaContable { get; set; } = null!;
        public string DescripcionLinea { get; set; } = null!;
        public double Debito { get; set; }
        public double Credito { get; set; }
        public double Balance { get; set; }
    }
}
