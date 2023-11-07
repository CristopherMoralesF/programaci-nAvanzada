using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class AccountsResume
    {
        public string IdCuenta { get; set; } = null!;
        public string DescripcionCuenta { get; set; } = null!;
        public int IdCategoria { get; set; }
    }
}
