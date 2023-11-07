using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class AsientoLinea
    {
        public int IdAsientoLinea { get; set; }
        public int IdAsiento { get; set; }
        public string IdCuentaContable { get; set; } = null!;
        public string DescripcionLinea { get; set; } = null!;
        public double Debito { get; set; }
        public double Credito { get; set; }

        public virtual Asiento IdAsientoNavigation { get; set; } = null!;
        public virtual CuentaContable IdCuentaContableNavigation { get; set; } = null!;
    }
}
