using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _context;


        public EstadoController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        [Route("api/estadoActivo/consultarEstados")]
        [HttpGet]
        public List<EstadoEnt> consultarEstados()
        {
            List<EstadoEnt> listaEstados = new List<EstadoEnt>();

            foreach (var estado in _context.Estados.ToList())
            {
                listaEstados.Add(new EstadoEnt
                {
                    idEstado = estado.IdEstado,
                    descripcionEstado = estado.DescripcionEstado
                });
            }

            return listaEstados;
        }

        [Route("api/estadoActivo/consultarEstadoActivos")]
        [HttpGet]
        public EstadoEnt consultarEstadoActivos(int idEstado)
        {
            var resultado = (from x in _context.Estados
                             where x.IdEstado == idEstado
                             select x).FirstOrDefault();

            EstadoEnt estadoOutput = new EstadoEnt();

            estadoOutput.idEstado = resultado.IdEstado;
            estadoOutput.descripcionEstado = resultado.DescripcionEstado; 

            return estadoOutput;
        }
    }
}
