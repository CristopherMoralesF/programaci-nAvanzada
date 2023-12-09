using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace assetManagementClassLibrary
{

    [Table("USUARIO")]

    public class UsuariosEnt
    {
        [Key]
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public int idRole { get; set; }
        public int estado { get; set; }
        public int estadoContrasenna { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public string role { get; set; }


    }
}