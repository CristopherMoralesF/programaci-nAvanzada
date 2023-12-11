using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivoController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _context;

        public ActivoController(ASSET_MANAGEMENTContext context)
        {
            _context = context;

        }

        [Route("consultarActivo")]
        [HttpGet]
        public ActivoEnt consultarActivo(int idActivo)
        {
            ClasesController clasesController = new ClasesController(_context);
            UsuariosController usuariosController = new UsuariosController(_context);
            ubicacionController ubicacionController = new ubicacionController(_context);
            EstadoController estadoController = new EstadoController(_context);

            var activo = (from x in _context.Activos
                          where
                              x.IdActivo.Equals(idActivo)
                          select x).FirstOrDefault();

            ActivoEnt activoOutput = new ActivoEnt();

            activoOutput.idActivo = activo.IdActivo;
            activoOutput.claseActivo = clasesController.consultarClase(activo.IdClase);
            activoOutput.ubicacionActivo = ubicacionController.consultarUbicacionActivo(activo.IdUbicacion);
            activoOutput.duennoActivo = usuariosController.consultarUsuario(activo.IdDuenno);
            activoOutput.estadoActivo = estadoController.consultarEstadoActivos(activo.IdEstado);
            activoOutput.descripcionActivo = activo.DescripcionActivo;
            activoOutput.valorAdquisicion = Convert.ToDouble(activo.ValorAdquisicion);
            activoOutput.fechaAdquiscion = (DateTime)activo.FechaAdquisicion;
            activoOutput.periodosDepreciados = (int)activo.PeriodosDepreciados;
            activoOutput.validacionesActivo = consultarValidacionesActivo(activo.IdActivo);

            return activoOutput;
        }

        [HttpGet]
        public List<ValidacionClaseEnt> consultarValidacionesActivo(int idActivo)
        {
                var validaciones = (from x in _context.ResumenValidacionesCompletas
                                    where x.IdActivo == idActivo
                                    select x).ToList();

                List<ValidacionClaseEnt> validacionesActivo = new List<ValidacionClaseEnt>();

                foreach (var validacion in validaciones)
                {
                    validacionesActivo.Add(new ValidacionClaseEnt
                    {
                        idValidacion = validacion.IdTipoValidacion,
                        descripcionValidacion = validacion.DescripcionValidacion,
                        valor = validacion.Valor
                    });
                }

                return validacionesActivo;
            
        }

        [Route("consultarActivos")]
        [HttpGet]
        public List<ActivoEnt> consultarActivos()
        {
            ClasesController clasesController = new ClasesController(_context);
            UsuariosController usuariosController = new UsuariosController(_context);
            ubicacionController ubicacionController = new ubicacionController(_context);
            EstadoController estadoController = new EstadoController(_context);

            List<ActivoEnt> listaActivos = new List<ActivoEnt>();

                foreach (var activo in _context.Activos.ToList())
                {
                   

                    listaActivos.Add(new ActivoEnt
                        {
                            idActivo = activo.IdActivo,
                            claseActivo = clasesController.consultarClase(activo.IdClase),
                            ubicacionActivo = ubicacionController.consultarUbicacionActivo(activo.IdUbicacion),
                            duennoActivo = usuariosController.consultarUsuario(activo.IdDuenno),
                            estadoActivo = estadoController.consultarEstadoActivos(activo.IdEstado),
                            descripcionActivo = activo.DescripcionActivo,
                            valorAdquisicion = Convert.ToDouble(activo.ValorAdquisicion),
                            fechaAdquiscion = (DateTime)activo.FechaAdquisicion,
                            periodosDepreciados = (int)activo.PeriodosDepreciados
                        });
                }


                return listaActivos; 
        }

        [Route("consultarAuxiliarActivos")]
        [HttpGet]
        public List<AuxiliarEnt> consultarAuxiliarActivos()
        {

            List<AuxiliarEnt> listaActivos = new List<AuxiliarEnt>();

            foreach (var item in _context.AuxiliarDepreciacions)
            {
                listaActivos.Add(new AuxiliarEnt
                {
                    idActivo = item.IdActivo,
                    descripcionActivo = item.DescripcionActivo,
                    valorAdquisicion = item.ValorAdquisicion,
                    fechaAdquisicion = (DateTime)item.FechaAdquisicion,
                    periodosDepreciados = (int)item.PeriodosDepreciados,
                    descripcionClase = item.DescripcionClase,
                    vidaUtil = (int)item.VidaUtil,
                    depreciacionMensual = Convert.ToDouble(item.DepreciacionMensual),
                    depreciacionAcumulada = Convert.ToDouble(item.DepreciacionAcumulada)
                });
            }

            return listaActivos;
        }

        [Route("consultarActivosUsuario")]
        [HttpGet]
        public List<ActivoEnt> consultarActivosUsuario(int idUsuario)
        {
            ClasesController clasesController = new ClasesController(_context);
            UsuariosController usuariosController = new UsuariosController(_context);
            ubicacionController ubicacionController = new ubicacionController(_context);
            EstadoController estadoController = new EstadoController(_context);

            List<ActivoEnt> listaActivos = new List<ActivoEnt>();

            var resultado = (from x in _context.Activos
                             where x.IdDuenno == idUsuario
                             select x).ToList();

            foreach (var activo in resultado)
            {
                listaActivos.Add(new ActivoEnt
                {
                    idActivo = activo.IdActivo,
                    claseActivo = clasesController.consultarClase(activo.IdClase),
                    ubicacionActivo = ubicacionController.consultarUbicacionActivo(activo.IdUbicacion),
                    duennoActivo = usuariosController.consultarUsuario(activo.IdDuenno),
                    estadoActivo = estadoController.consultarEstadoActivos(activo.IdEstado),
                    descripcionActivo = activo.DescripcionActivo,
                    valorAdquisicion = Convert.ToDouble(activo.ValorAdquisicion),
                    fechaAdquiscion = (DateTime)activo.FechaAdquisicion,
                    periodosDepreciados = (int)activo.PeriodosDepreciados
                });
            }


            return listaActivos;
        }

        [Route("crearActivo")]
        [HttpPost]
        public int crearActivo(CrearActivoEnt activo)
        {
            Activo activoDB = new Activo
            {
                IdActivo = (int)activo.idActivo,
                IdClase = activo.idClase,
                IdUbicacion = activo.idUbicacíon,
                IdDuenno = activo.idUsuario,
                IdEstado = activo.idEstado,
                DescripcionActivo = activo.descripcionActivo,
                ValorAdquisicion = (double)activo.valorAdquisicion,
                FechaAdquisicion = activo.fechaAdquiscion,
                PeriodosDepreciados = activo.periodosDepreciados
            };

            _context.Add(activoDB);

            return _context.SaveChanges();
        }

        [Route("modificarClase")]
        [HttpPut]
        public int actualizarClaseACtivo(ModificarActivoClaseEnt activo)
        {
            _context.Database.ExecuteSqlRaw("EXEC MODIFICAR_CLASE {0}, {1}", activo.idActivo, activo.idClase);
            return _context.SaveChanges(); 
        }
        
    }
}
