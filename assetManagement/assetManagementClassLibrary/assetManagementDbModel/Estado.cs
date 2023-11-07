using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Estado
    {
        public Estado()
        {
            Activos = new HashSet<Activo>();
        }

        public int IdEstado { get; set; }
        public string DescripcionEstado { get; set; } = null!;

        public virtual ICollection<Activo> Activos { get; set; }
    }
}
