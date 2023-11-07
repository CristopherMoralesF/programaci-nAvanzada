using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            Activos = new HashSet<Activo>();
        }

        public int IdUbicacion { get; set; }
        public string IdEdificio { get; set; } = null!;
        public string DescripcionSeccion { get; set; } = null!;

        public virtual Edificio IdEdificioNavigation { get; set; } = null!;
        public virtual ICollection<Activo> Activos { get; set; }
    }
}
