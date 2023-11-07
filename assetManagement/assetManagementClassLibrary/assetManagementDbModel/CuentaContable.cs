using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class CuentaContable
    {
        public CuentaContable()
        {
            AsientoLineas = new HashSet<AsientoLinea>();
            ClaseCuenta = new HashSet<ClaseCuentum>();
        }

        public string IdCuenta { get; set; } = null!;
        public string DescripcionCuenta { get; set; } = null!;
        public int IdCategoria { get; set; }
        public double? TotalDebitos { get; set; }
        public double? TotalCreditos { get; set; }
        public double? Balance { get; set; }
        public string Naturaleza { get; set; } = null!;

        public virtual CategoriaCuentum IdCategoriaNavigation { get; set; } = null!;
        public virtual ICollection<AsientoLinea> AsientoLineas { get; set; }
        public virtual ICollection<ClaseCuentum> ClaseCuenta { get; set; }
    }
}
