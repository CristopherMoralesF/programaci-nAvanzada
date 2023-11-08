using assetManagementClassLibrary.assetManagementDbModel;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_API.Entities;

namespace assetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientoController : ControllerBase
    {

        private readonly ASSET_MANAGEMENTContext _context;

        public AsientoController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("consultarAsientos")]
        public List<Asiento> consultarAsientos()
        {
            return _context.Asientos.ToList();  
        }

        [HttpGet]
        [Route("consultarAsiento")]
        public IActionResult consultarAsiento(int idAsiento)
        {
            //Get the items from the data base
            var asiento = (from x in _context.Asientos where x.IdAsiento == idAsiento select x).FirstOrDefault(); 
            List<AsientoLinea> asientosLineas = (from x in _context.AsientoLineas where x.IdAsiento == idAsiento select x).ToList();

            // Select the variables who will store the information. 
            AsientoEnt asientoOutput = new AsientoEnt();
            List <AsientoLineasEnt> asientoBody = new List<AsientoLineasEnt>();

            if (asiento == null)
            {
                return NotFound("Journal Entry not found"); 
            } else
            {
                foreach (var linea in asientosLineas)
                {
                    asientoBody.Add(new AsientoLineasEnt
                    {
                        idAsientoLinea = linea.IdAsientoLinea,
                        idCuentaContable = linea.IdCuentaContable,
                        descripcionLinea = linea.DescripcionLinea,
                        debito = Convert.ToDouble(linea.Debito),
                        credito = Convert.ToDouble(linea.Credito),
                    });
                }

                if (asiento.IdClase == null)
                {
                    asientoOutput.clase = new ClaseEnt { idClase = 0 };
                }
                else
                {
                    asientoOutput.clase = new ClaseEnt { idClase = (int)asiento.IdClase };
                }

                asientoOutput.idAsiento = asiento.IdAsiento;
                asientoOutput.fecha = asiento.Fecha;
                asientoOutput.descripcion = asiento.Descripcion;
                asientoOutput.cuerpoAsiento = asientoBody;

                return Ok(asientoOutput);
            }

        }


    }
}
