using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class Usuario
    {
        public Usuario()
        {
            Activos = new HashSet<Activo>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasenna { get; set; } = null!;
        public int IdRole { get; set; }
        public int Estado { get; set; }
        public int EstadoContrasenna { get; set; }

        public virtual UsuarioRole IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Activo> Activos { get; set; }
    }
}
