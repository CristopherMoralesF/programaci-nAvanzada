using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class AccountsBalanceResume
    {
        public string IdCuenta { get; set; } = null!;
        public string DescripcionCuenta { get; set; } = null!;
        public string Naturaleza { get; set; } = null!;
        public string? DescripcionCategoria { get; set; }
        public double? TotalDebitos { get; set; }
        public double? TotalCreditos { get; set; }
        public double? Balance { get; set; }
    }
}
