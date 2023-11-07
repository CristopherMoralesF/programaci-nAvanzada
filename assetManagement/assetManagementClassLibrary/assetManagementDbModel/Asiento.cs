using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Asiento
    {
        public Asiento()
        {
            AsientoLineas = new HashSet<AsientoLinea>();
        }

        public int IdAsiento { get; set; }
        public int? IdClase { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual Clase? IdClaseNavigation { get; set; }
        public virtual ICollection<AsientoLinea> AsientoLineas { get; set; }
    }
}
