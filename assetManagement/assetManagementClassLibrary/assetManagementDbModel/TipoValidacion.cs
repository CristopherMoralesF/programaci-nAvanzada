using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class TipoValidacion
    {
        public TipoValidacion()
        {
            Validacions = new HashSet<Validacion>();
        }

        public int IdTipoValidacion { get; set; }
        public int IdClase { get; set; }
        public string DescripcionValidacion { get; set; } = null!;

        public virtual Clase IdClaseNavigation { get; set; } = null!;
        public virtual ICollection<Validacion> Validacions { get; set; }
    }
}
