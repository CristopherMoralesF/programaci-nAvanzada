using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class CategoriaCuentum
    {
        public CategoriaCuentum()
        {
            ClaseCuenta = new HashSet<ClaseCuentum>();
            CuentaContables = new HashSet<CuentaContable>();
        }

        public int IdCategoria { get; set; }
        public string? DescripcionCategoria { get; set; }

        public virtual ICollection<ClaseCuentum> ClaseCuenta { get; set; }
        public virtual ICollection<CuentaContable> CuentaContables { get; set; }
    }
}
