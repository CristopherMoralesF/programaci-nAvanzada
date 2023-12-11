using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace assetManagementApi.Controllers
{
    public class IndicadoresController : Controller
    {

        private readonly ASSET_MANAGEMENTContext _context;

        public IndicadoresController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        public List<IndicadoresRiesgoEnt> validacionRiesgoActivosUsuario(int idUsuario)
        {

            var validaciones = (from x in _context.ValidacionRiesgoActivos
                                where x.IdDuenno == idUsuario
                                select x).ToList();

            List<IndicadoresRiesgoEnt> indicadores = new List<IndicadoresRiesgoEnt>();

            foreach (var validacion in validaciones)
            {
                indicadores.Add(new IndicadoresRiesgoEnt
                {
                    idActivo = validacion.IdActivo,
                    idDuenno = validacion.IdDuenno,
                    descripcionActivo = validacion.DescripcionActivo,
                    porcentajeValidacion = Convert.ToDouble(validacion.ValidationPercentaje),
                    validacionRiesgo = validacion.ValidacionRiesgo,
                    descripcionClase = validacion.DescripcionClase
                });
            }

            return indicadores;

        }

        public List<IndicadoresRiesgoEnt> validacionRiesgoCompannia()
        {

            List<IndicadoresRiesgoEnt> indicadores = new List<IndicadoresRiesgoEnt>();

            foreach (var validacion in _context.ValidacionRiesgoActivos)
            {
                indicadores.Add(new IndicadoresRiesgoEnt
                {
                    idActivo = validacion.IdActivo,
                    idDuenno = validacion.IdDuenno,
                    descripcionActivo = validacion.DescripcionActivo,
                    porcentajeValidacion = Convert.ToDouble(validacion.ValidationPercentaje),
                    validacionRiesgo = validacion.ValidacionRiesgo
                });
            }

            return indicadores;

        }

        public List<ClaseEnt> resumenClases()
        {

            List<ClaseEnt> listaClases = new List<ClaseEnt>();

            foreach (var clase in _context.ResumenActivosClases.ToList())
            {
                listaClases.Add(new ClaseEnt
                {
                    idClase = clase.IdClase,
                    descripcionClase = clase.DescripcionClase,
                    totalActivos = (int)clase.TotalActivos,
                });
            }

            return listaClases;
            
        }

        public List<ClaseEnt> resumenClasesRiesgo()
        {

                List<ClaseEnt> listaRiesgosClase = new List<ClaseEnt>();

                foreach (var clase in _context.ValidacionRiesgoClases)
                {
                    listaRiesgosClase.Add(new ClaseEnt
                    {
                        idClase = clase.IdClase,
                        descripcionClase = clase.DescripcionClase,
                        categorizacionRiesgo = clase.ValidacionRiesgo,
                        totalActivos = (int)clase.TotalActivos  
                    });

                }

                return listaRiesgosClase;
           
        }

        [Route("api/indicadores/optenerIndicadores")]
        [HttpGet]
        public IndicadoresEnt optenerIndicadores(int idUsuario)
        {

            var resumenActivos = _context.Activos
                .Select(a => new
                {
                    a.IdActivo,
                    a.ValorAdquisicion,
                    PorcentajeCumplimiento = ((double)_context.Validacions.Count(v => v.IdActivo == a.IdActivo) /
                                              _context.TipoValidacions.Count(tv => tv.IdClase == a.IdClase) * 100),
                    TotalActivosUsuario = (a.IdDuenno == idUsuario) ? 1 : 0
                }).ToList();

            IndicadoresEnt indicadores = new IndicadoresEnt();
            indicadores.totalActivos = resumenActivos.Count();
            indicadores.totalInversion = resumenActivos.Sum(a => a.ValorAdquisicion);
            indicadores.porcentajeCumplimineto = resumenActivos.Average(a => a.PorcentajeCumplimiento);
            indicadores.totalActivosUsuario = resumenActivos.Sum(a => a.TotalActivosUsuario);
            indicadores.activosUsuario = validacionRiesgoActivosUsuario(idUsuario);
            indicadores.activosCompannia = validacionRiesgoCompannia();
            indicadores.resumenClases = resumenClases();
            indicadores.resumenClasesRiesgo = resumenClasesRiesgo();


            return indicadores;
        }
    }
}
