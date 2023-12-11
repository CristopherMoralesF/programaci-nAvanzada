using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ubicacionController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _context;

        public ubicacionController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        [Route("api/ubicacionActivo/consultarUbicaciones")]
        [HttpGet]
        public List<UbicacionEnt> consultarUbicaciones()
        {
            List<UbicacionEnt> ubicaciones = new List<UbicacionEnt>();

            foreach (var ubicacion in _context.Ubicacions.ToList())
            {

                ubicaciones.Add(new UbicacionEnt
                {
                    idUbicacíon = ubicacion.IdUbicacion,
                    idEdificio = ubicacion.IdEdificio,
                    descripcionSeccion = ubicacion.DescripcionSeccion
                });

            }

            return ubicaciones;
        }

        [Route("api/ubicacionActivo/consultarUbicacionActivo")]
        [HttpGet]
        public UbicacionEnt consultarUbicacionActivo(int idUbicacion)
        {
            var resultado = (from x in _context.Ubicacions where x.IdUbicacion == idUbicacion select x).FirstOrDefault();

            UbicacionEnt ubicacionOutput = new UbicacionEnt
            {
                idUbicacíon = resultado.IdUbicacion,
                idEdificio = resultado.IdEdificio,
                descripcionSeccion = resultado.DescripcionSeccion
            };

            return ubicacionOutput;
        }
    }
}
