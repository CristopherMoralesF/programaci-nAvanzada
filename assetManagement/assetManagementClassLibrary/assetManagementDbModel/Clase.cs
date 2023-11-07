using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Clase
    {
        public Clase()
        {
            Activos = new HashSet<Activo>();
            Asientos = new HashSet<Asiento>();
            ClaseCuenta = new HashSet<ClaseCuentum>();
            TipoValidacions = new HashSet<TipoValidacion>();
        }

        public int IdClase { get; set; }
        public string? DescripcionClase { get; set; }
        public int? VidaUtil { get; set; }

        public virtual ICollection<Activo> Activos { get; set; }
        public virtual ICollection<Asiento> Asientos { get; set; }
        public virtual ICollection<ClaseCuentum> ClaseCuenta { get; set; }
        public virtual ICollection<TipoValidacion> TipoValidacions { get; set; }
    }
}
