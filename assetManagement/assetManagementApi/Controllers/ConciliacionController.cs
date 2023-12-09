using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConciliacionController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _context;

        public ConciliacionController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("consultarConciliacion")]
        public IActionResult Index()
        {

            //Declare the list that will be sent
            List<ConciliacionEnt> conciliacion = new List<ConciliacionEnt>();

            foreach (var resultado in _context.ConciliacionBalanceClases.ToList())
            {
                conciliacion.Add(new ConciliacionEnt
                {
                    idClase = resultado.IdClase,
                    idCuenta = resultado.IdCuenta,
                    categoriaCuenta = resultado.CategoriaCuenta,
                    descripcionClase = resultado.DescripcionClase,
                    balance = Convert.ToDouble(resultado.Balance),
                    valorAuxiliar = Convert.ToDouble(resultado.ValorAuxiliar),
                    diferencia = Convert.ToDouble(resultado.Diferencia),
                });
            }


            return Ok(conciliacion);
        }

    }
}
