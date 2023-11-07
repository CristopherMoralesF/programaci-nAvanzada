using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Edificio
    {
        public Edificio()
        {
            Ubicacions = new HashSet<Ubicacion>();
        }

        public string IdEdificio { get; set; } = null!;
        public string DescripcionEdificio { get; set; } = null!;

        public virtual ICollection<Ubicacion> Ubicacions { get; set; }
    }
}
