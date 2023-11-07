using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Activo
    {
        public Activo()
        {
            Validacions = new HashSet<Validacion>();
        }

        public int IdActivo { get; set; }
        public int IdClase { get; set; }
        public int IdUbicacion { get; set; }
        public int IdDuenno { get; set; }
        public int IdEstado { get; set; }
        public string? DescripcionActivo { get; set; }
        public double ValorAdquisicion { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public int? PeriodosDepreciados { get; set; }

        public virtual Clase IdClaseNavigation { get; set; } = null!;
        public virtual Usuario IdDuennoNavigation { get; set; } = null!;
        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual Ubicacion IdUbicacionNavigation { get; set; } = null!;
        public virtual ICollection<Validacion> Validacions { get; set; }
    }
}
