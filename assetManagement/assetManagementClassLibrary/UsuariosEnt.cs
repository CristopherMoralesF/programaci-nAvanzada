using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_API.Entities
{
    public class UsuariosEnt
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public string role { get; set; }    
        public int idRole { get; set; }
        public int estado { get; set; }
        public int estadoContrasenna { get; set; }
        public string Token { get; set; }

    }
}