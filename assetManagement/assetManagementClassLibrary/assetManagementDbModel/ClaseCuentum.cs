using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ClaseCuentum
    {
        public int IdClaseCuenta { get; set; }
        public int IdClase { get; set; }
        public string IdCuenta { get; set; } = null!;
        public int IdCategoriaCuenta { get; set; }

        public virtual CategoriaCuentum IdCategoriaCuentaNavigation { get; set; } = null!;
        public virtual Clase IdClaseNavigation { get; set; } = null!;
        public virtual CuentaContable IdCuentaNavigation { get; set; } = null!;
    }
}
