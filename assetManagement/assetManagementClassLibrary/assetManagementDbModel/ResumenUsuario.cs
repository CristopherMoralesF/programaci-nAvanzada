using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ResumenUsuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasenna { get; set; } = null!;
        public int IdRole { get; set; }
        public int Estado { get; set; }
        public string? NombreRole { get; set; }
    }
}
