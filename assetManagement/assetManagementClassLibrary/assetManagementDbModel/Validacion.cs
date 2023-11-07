using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Validacion
    {
        public int IdValidacion { get; set; }
        public int IdTipoValidacion { get; set; }
        public int IdActivo { get; set; }
        public string? Valor { get; set; }

        public virtual Activo IdActivoNavigation { get; set; } = null!;
        public virtual TipoValidacion IdTipoValidacionNavigation { get; set; } = null!;
    }
}
