
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Proyecto_API.Entities;

namespace assetManagementApi.Controllers
{


    namespace assetManagementApi.Controllers
    {
        [Authorize]
        public class ActivoController : ControllerBase
        {
            private readonly ASSET_MANAGEMENTContext _context;


            public ActivoController(ASSET_MANAGEMENTContext context)
            {
                _context = context;
            }



            [Route("api/consultarActivo")]
            [HttpGet]
            public ActivoEnt consultarActivo(int idActivo)
            {
                return consultarActivo(idActivo);
            }

            [Route("api/obtenerListaActivos")]
            [HttpGet]
            public List<ActivoEnt> obtenerListaActivos()
            {
                return obtenerListaActivos();
            }

            [Route("api/crearActivo")]
            [HttpPost]
            public int crearActivo(ActivoEnt nuevoActivo)
            {
                return crearActivo(nuevoActivo);
            }

            [Route("api/consultarValidacionesActivo")]
            [HttpGet]
            public List<ActivoEnt> consultarValidacionesActivo()
            {
                return consultarValidacionesActivo();
            }

            [Route("api/crearValidacionActivo")]
            [HttpPost]
            public int crearValidacionActivo(ActivoEnt nuevaValidacion)
            {
                return crearValidacionActivo(nuevaValidacion);
            }


            [Route("api/activo/modificarClase")]
            [HttpPut]
            public int actualizarClaseACtivo(ActivoEnt activo)
            {
                return actualizarClaseACtivo(activo);
            }
        }
    }
}
