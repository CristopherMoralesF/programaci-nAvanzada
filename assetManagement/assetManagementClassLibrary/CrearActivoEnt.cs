using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assetManagementClassLibrary
{
    public class CrearActivoEnt
    {
        public int idActivo { get; set; }
        public int idClase { get; set; }

        public int idUbicacíon { get; set; }
        public int idUsuario { get; set; }

        public int idEstado { get; set; }
        public string descripcionActivo { get; set; }
        public double valorAdquisicion { get; set; }
        public DateTime fechaAdquiscion { get; set; }
        public int periodosDepreciados { get; set; }
    }
}
