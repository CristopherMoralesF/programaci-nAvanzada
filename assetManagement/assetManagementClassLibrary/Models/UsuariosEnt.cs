using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace assetManagementClassLibrary.Models
{
    public class UsuariosEnt
    {
        [Key]
        public int ID_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string CORREO { get; set; }
        public string CONTRASENNA { get; set; }

        public int ID_ROLE { get; set; }
        public int ESTADO { get; set; }
        public int ESTADO_CONTRASENNA { get; set; }

        [NotMapped]
        public string role { get; set; }

    }
}