using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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

        [Route("agregarValidacionActivo")]
        [HttpPost]
        public int completarValidacion(ValidacionClaseEnt nuevaValidacion)
        {
            Validacion validacion = new Validacion();

            validacion.IdTipoValidacion = nuevaValidacion.idValidacion;
            validacion.IdActivo = nuevaValidacion.idActivo;
            validacion.Valor = nuevaValidacion.valor;

            _context.Add(validacion);
            return _context.SaveChanges();
        }

        [Route("modificarValidacionActivo")]
        [HttpPost]
        public int modificarValidacion(ValidacionClaseEnt validacion)
        {
            var validacionEditar = (from x in _context.Validacions
                                    where x.IdTipoValidacion == validacion.idValidacion && x.IdActivo == validacion.idActivo
                                    select x).FirstOrDefault();

            validacionEditar.Valor = validacion.valor;

            return _context.SaveChanges();
        }

    }
}
