using assetManagementClassLibrary.assetManagementDbModel;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementApi.Controllers
{
    public class ClasesController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _context;


        public ClasesController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }


        [Route("api/ejecutarDepreciacionClase")]
        [HttpPost]
        public List<AuxiliarEnt> ejecutarDepreciacionClase([FromBody] ClaseEnt clase)
        {
            //Run depreciation

            //_context.CORRER_DEPRECIACION(clase.idClase, clase.asientoDepreciacion.descripcion);
            _context.Database.ExecuteSqlInterpolated($"EXEC CORRER_DEPRECIACION {clase.idClase}, {clase.asientoDepreciacion.descripcion}");

            //Get the assets subledger with the new depreciation. 
            var resultados = (from x in _context.AuxiliarDepreciacions
                              where x.IdClase == clase.idClase
                              select x).ToList();

            //Create the object to return the result
            List<AuxiliarEnt> nuevoAuxiliar = new List<AuxiliarEnt>();

            foreach (var linea in resultados)
            {
                nuevoAuxiliar.Add(new AuxiliarEnt
                {
                    descripcionActivo = linea.DescripcionActivo,
                    valorAdquisicion = linea.ValorAdquisicion,
                    fechaAdquisicion = (DateTime)linea.FechaAdquisicion,
                    periodosDepreciados = (int)linea.PeriodosDepreciados,
                    descripcionClase = linea.DescripcionClase,
                    vidaUtil = (int)linea.VidaUtil,
                    idClase = linea.IdClase,
                    depreciacionMensual = Convert.ToDouble(linea.DepreciacionMensual),
                    depreciacionAcumulada = Convert.ToDouble(linea.DepreciacionAcumulada)
                });
            }

            return nuevoAuxiliar;

        }


        public List<ValidacionClaseEnt> consultarValidacionesClase(int idClase)
        {

            var validacionesClase = (from x in _context.TipoValidacions
                                     where x.IdClase == idClase
                                     select x).ToList();

            List<ValidacionClaseEnt> listaValidaciones = new List<ValidacionClaseEnt>();


            foreach (var validacion in validacionesClase)
            {

                listaValidaciones.Add(new ValidacionClaseEnt
                {
                    idValidacion = validacion.IdTipoValidacion,
                    descripcionValidacion = validacion.DescripcionValidacion
                });
            }


            return listaValidaciones;

        }


        [Route("api/consultarClase")]
        [HttpGet]
        public ClaseEnt consultarClase(int idClase)
        {
            var clase = (from x in _context.Clases
                         where x.IdClase == idClase
                         select x).FirstOrDefault();

            ClaseEnt claseOutput = new ClaseEnt();

            claseOutput.idClase = clase.IdClase;
            claseOutput.descripcionClase = clase.DescripcionClase;
            claseOutput.vidaUtil = (int)clase.VidaUtil;
            claseOutput.listaValidaciones = consultarValidacionesClase(clase.IdClase);

            return claseOutput;
        }

        [Route("api/obtenerListaClases")]
        [HttpGet]
        public List<ClaseEnt> obtenerListaClases()
        {

            {
                //Create the variable to populate all classes information. 
                List<ClaseEnt> clases = new List<ClaseEnt>();

                //Recover the classes from the database and include each line in the output variable. 
                foreach (var clase in _context.ClassesLists.ToList())
                {
                    clases.Add(new ClaseEnt
                    {
                        idClase = clase.IdClase,
                        descripcionClase = clase.DescripcionClase,
                        vidaUtil = (int)clase.VidaUtil,
                        cuentaActivo = new CuentaContableEnt { idCuenta = clase.CuentaActivo },
                        cuentaGasto = new CuentaContableEnt { idCuenta = clase.CuentaGasto },
                        cuentaDepAcumulada = new CuentaContableEnt { idCuenta = clase.CuentaDepAcumulada }
                    });
                }

                return clases;

            }
        }

        [Route("api/crearClase")]
        [HttpPost]
        public IActionResult crearClase([FromBody] ClaseEnt nuevaClase)
        {
            _context.Database.ExecuteSqlInterpolated($"EXEC CREAR_CLASE {nuevaClase.descripcionClase}, {nuevaClase.cuentaActivo.idCuenta}, {nuevaClase.cuentaDepAcumulada.idCuenta}, {nuevaClase.cuentaGasto.idCuenta}, {nuevaClase.vidaUtil}");
            return Ok("Clase agregada exitosamente");
        }

        [Route("api/consultarValidacionesClase")]
        [HttpGet]
        public List<ClaseEnt> consultarValidacionesClase()
        {

            List<ClaseEnt> validaciones = new List<ClaseEnt>();

            foreach (var validacion in _context.ValidacionesResumen.ToList())
            {

                validaciones.Add(new ClaseEnt
                {
                    idClase = validacion.IdClase,
                    descripcionClase = validacion.DescripcionClase,
                    validacionClase = new ValidacionClaseEnt
                    {
                        idValidacion = validacion.IdTipoValidacion,
                        descripcionValidacion = validacion.DescripcionValidacion
                    }

                });

            }

            return validaciones;

        }

        [Route("api/crearValidacionClase")]
        [HttpPost]
        public int crearValidacionClase(ClaseEnt clase)
        {
            TipoValidacion VALIDACION = new TipoValidacion();

            VALIDACION.IdClase = clase.idClase;
            VALIDACION.DescripcionValidacion = clase.validacionClase.descripcionValidacion;

            _context.TipoValidacions.Add(VALIDACION);

            return _context.SaveChanges();
        }
    }
}
