using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_API.Entities
{
    public class ErrorEnt
    {
        public int idError { get; set; }
        public string pantalla { get; set; }
        public string error { get; set; }
        public DateTime fecha { get; set; } 
    }
}