using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Web;

namespace assetManagementClassLibrary
{
    public class AsientoEnt
    {
        public int idAsiento {get;set;}
        public ClaseEnt clase { get;set;}
        public DateTime fecha { get;set;}
        public string descripcion { get;set;}
        public List<AsientoLineasEnt> cuerpoAsiento { get;set;}
    }
}