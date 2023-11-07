using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class UsuarioRole
    {
        public UsuarioRole()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRole { get; set; }
        public string? NombreRole { get; set; }
        public string? DescripcionPermisos { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
