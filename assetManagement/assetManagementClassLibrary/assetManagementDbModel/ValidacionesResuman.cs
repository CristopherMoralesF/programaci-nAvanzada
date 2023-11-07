using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class ValidacionesResuman
    {
        public int IdTipoValidacion { get; set; }
        public int IdClase { get; set; }
        public string DescripcionValidacion { get; set; } = null!;
        public string? DescripcionClase { get; set; }
    }
}
