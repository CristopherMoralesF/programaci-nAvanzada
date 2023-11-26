using assetManagementClassLibrary.assetManagementDbModel;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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
        public IActionResult consultarAsientos()
        {

            List<AsientoEnt> asientos = new List<AsientoEnt>();

            foreach (var asiento in _context.Asientos.ToList())
            {
                List<AsientoLineasEnt> lineasAsiento = new List<AsientoLineasEnt>();

                var asientoCuerpo = (from x in _context.AsientoLineas where x.IdAsiento == asiento.IdAsiento select x).ToList();

                foreach (var linea in asientoCuerpo)
                {
                    lineasAsiento.Add(new AsientoLineasEnt
                    {
                        idAsientoLinea = linea.IdAsientoLinea,
                        idCuentaContable = linea.IdCuentaContable,
                        descripcionLinea = linea.DescripcionLinea,
                        debito = linea.Debito,
                        credito = linea.Credito,    
                    });

                }

                if (asiento.IdClase == null)
                {
                    asientos.Add(new AsientoEnt
                    {
                        idAsiento = asiento.IdAsiento,
                        clase = new ClaseEnt { idClase = 0, descripcionClase = "No Activos" },
                        fecha = asiento.Fecha,
                        descripcion = asiento.Descripcion,
                        cuerpoAsiento = lineasAsiento
                    });

                } else
                {

                    var claseActivo = (from x in _context.Clases where x.IdClase == asiento.IdClase select x).FirstOrDefault();


                    asientos.Add(new AsientoEnt
                    {
                        idAsiento = asiento.IdAsiento,
                        clase = new ClaseEnt
                        {
                            idClase = claseActivo.IdClase,
                            descripcionClase = claseActivo.DescripcionClase,
                            vidaUtil = (int)claseActivo.VidaUtil
                        },
                        fecha = asiento.Fecha,
                        descripcion = asiento.Descripcion,
                        cuerpoAsiento = lineasAsiento
                    });
                }


            }

            return Ok(asientos);  
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
