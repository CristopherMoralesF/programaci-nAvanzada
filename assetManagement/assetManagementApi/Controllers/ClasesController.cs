using assetManagementClassLibrary.assetManagementDbModel;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _context;

        public ClasesController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        [Route("ejecutarDepreciacionClase")]
        [HttpPost]
        public List<AuxiliarEnt> ejecutarDepreciacionClase(ClaseDepreciacionEnt clase)
        {
            _context.Database.ExecuteSqlRaw("EXEC CORRER_DEPRECIACION {0}, {1}", clase.idClase, clase.descripcionClase);

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


        [Route("consultarClase")]
        [HttpGet]
        public ClaseEnt consultarClase(int idClase)
        {

            var clase = _context.Clases.FirstOrDefault(x => x.IdClase == idClase);

            ClaseEnt claseOutput = new ClaseEnt();

            claseOutput.idClase = clase.IdClase;
            claseOutput.descripcionClase = clase.DescripcionClase;
            claseOutput.vidaUtil = (int)clase.VidaUtil;
            claseOutput.listaValidaciones = consultarValidacionesClase(clase.IdClase);

            return claseOutput;
        }


        [Route("obtenerListaClases")]
        [HttpGet]
        public List<ClaseEnt> obtenerListaClases()
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



        [Route("crearClase")]
        [HttpPost]
        public int crearClase(NuevaClaseEnt nuevaClase)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC CREAR_CLASE {0}, {1}, {2}, {3}, {4}",
                nuevaClase.descripcionClase,
                nuevaClase.cuentaActivo,
                nuevaClase.cuentaDepAcumulada,
                nuevaClase.cuentaGasto,
                nuevaClase.vidaUtil
               );

                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
            

        }


        [Route("consultarValidacionesClase")]
        [HttpGet]
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
                    descripcionValidacion = validacion.DescripcionValidacion,
                });
            }

            return listaValidaciones;

        }

        
        [Route("crearValidacionClase")]
        [HttpPost]
        public int crearValidacionClase(NuevoTipoValidacionEnt nuevaValidacion)
        {
            TipoValidacion VALIDACION = new TipoValidacion();

            VALIDACION.IdClase = nuevaValidacion.IdClase;
            VALIDACION.DescripcionValidacion = nuevaValidacion.DescripcionValidacion; 

            _context.Add(VALIDACION);

            return _context.SaveChanges();
        }
        
    }
}
