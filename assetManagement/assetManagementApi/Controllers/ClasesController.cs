using assetManagement.Models;
using assetManagementApi.Models;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API.Entities;
using System.Collections.Generic;
using System.Web;

namespace assetManagementApi.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ClasesController : ControllerBase
    {

        private readonly ASSET_MANAGEMENTContext _context;


        public ClasesController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }
        ClasesModel clasesModel = new ClasesModel();

        [Route("api/ejecutarDepreciacionClase")]
        [HttpPost]
        public List<AuxiliarEnt> ejecutarDepreciacionClase(ClaseEnt clase)
        {
            return clasesModel.ejecutarDepreciacionClase(clase);
        }

        [Route("api/consultarClase")]
        [HttpGet]
        public ClaseEnt consultarClase(int idClase)
        {
            return clasesModel.consultarClase(idClase);
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
        public int crearClase([FromBody] ClaseEnt nuevaClase)
        {
            _context.Database.ExecuteSqlInterpolated($"EXEC CREAR_CLASE {nuevaClase.descripcionClase}, {nuevaClase.cuentaActivo.idCuenta}, {nuevaClase.cuentaDepAcumulada.idCuenta}, {nuevaClase.cuentaGasto.idCuenta}, {nuevaClase.vidaUtil}");
            return Ok("Clase creada exitosamente");

        }

        [Route("api/consultarValidacionesClase")]
        [HttpGet]
        public List<ClaseEnt> consultarValidacionesClase(int idClase)
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
