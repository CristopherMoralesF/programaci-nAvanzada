using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using assetManagementClassLibrary.assetManagementDbModel;
using assetManagementClassLibrary;

namespace assetManagement.Controllers
{
    public class ActivoesController : Controller
    {
        private readonly ASSET_MANAGEMENTContext _context;

        public ActivoesController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        // GET: Activoes
        public async Task<IActionResult> Index()
        {
            var aSSET_MANAGEMENTContext = _context.Activos.Include(a => a.IdClaseNavigation).Include(a => a.IdDuennoNavigation).Include(a => a.IdEstadoNavigation).Include(a => a.IdUbicacionNavigation);
            return View(await aSSET_MANAGEMENTContext.ToListAsync());
        }

        // GET: Activoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Activos == null)
            {
                return NotFound();
            }

            var activo = await _context.Activos
                .Include(a => a.IdClaseNavigation)
                .Include(a => a.IdDuennoNavigation)
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.IdActivo == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // GET: Activoes/Create
        public IActionResult Create()
        {
            ViewBag.IdClase = new SelectList(new[]
     {
        new { Id = 1, Nombre = "Vehículos" },
        new { Id = 2, Nombre = "Edificios" },
        new { Id = 3, Nombre = "Maquinaria" },
        new { Id = 4, Nombre = "Computadoras" }
    }, "Id", "Nombre");
            ViewData["IdDuenno"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasenna");
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "DescripcionEstado");
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "DescripcionSeccion");
            return View();
        }

        // POST: Activoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdActivo,IdClase,IdUbicacion,IdDuenno,IdEstado,DescripcionActivo,ValorAdquisicion,FechaAdquisicion,PeriodosDepreciados")] Activo activo)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(activo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IdClase = new SelectList(new[]
     {
        new { Id = 1, Nombre = "Vehículos" },
        new { Id = 2, Nombre = "Edificios" },
        new { Id = 3, Nombre = "Maquinaria" },
        new { Id = 4, Nombre = "Computadoras" }
    }, "Id", "Nombre");
            ViewData["IdDuenno"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasenna", activo.IdDuenno);
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "DescripcionEstado", activo.IdEstado);
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "DescripcionSeccion", activo.IdUbicacion);
            return View(activo);
        }

        // GET: Activoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Activos == null)
            {
                return NotFound();
            }

            var activo = await _context.Activos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }
            ViewBag.IdClase = new SelectList(new[]
    {
        new { Id = 1, Nombre = "Vehículos" },
        new { Id = 2, Nombre = "Edificios" },
        new { Id = 3, Nombre = "Maquinaria" },
        new { Id = 4, Nombre = "Computadoras" }
    }, "Id", "Nombre"); ViewData["IdDuenno"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasenna", activo.IdDuenno);
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "DescripcionEstado", activo.IdEstado);
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "DescripcionSeccion", activo.IdUbicacion);
            return View(activo);
        }

        // POST: Activoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdActivo,IdClase,IdUbicacion,IdDuenno,IdEstado,DescripcionActivo,ValorAdquisicion,FechaAdquisicion,PeriodosDepreciados")] Activo activo)
        {
            if (id != activo.IdActivo)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(activo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivoExists(activo.IdActivo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IdClase = new SelectList(new[]
     {
        new { Id = 1, Nombre = "Vehículos" },
        new { Id = 2, Nombre = "Edificios" },
        new { Id = 3, Nombre = "Maquinaria" },
        new { Id = 4, Nombre = "Computadoras" }
    }, "Id", "Nombre");
            ViewData["IdDuenno"] = new SelectList(_context.Usuarios, "IdUsuario", "Contrasenna", activo.IdDuenno);
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "DescripcionEstado", activo.IdEstado);
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "DescripcionSeccion", activo.IdUbicacion);
            return View(activo);
        }

        // GET: Activoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Activos == null)
            {
                return NotFound();
            }

            var activo = await _context.Activos
                .Include(a => a.IdClaseNavigation)
                .Include(a => a.IdDuennoNavigation)
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.IdActivo == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // POST: Activoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Activos == null)
            {
                return Problem("Entity set 'ASSET_MANAGEMENTContext.Activos'  is null.");
            }
            var activo = await _context.Activos.FindAsync(id);
            if (activo != null)
            {
                _context.Activos.Remove(activo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivoExists(int id)
        {
            return (_context.Activos?.Any(e => e.IdActivo == id)).GetValueOrDefault();
        }


        public IActionResult Principal()
        {


            return View();
        }

        //----------------------------------------------------------------------------------------detalleActivo

        public IActionResult detalleActivo()
        {


            return View();
        }


  

        public List<IndicadoresRiesgoEnt> ValidacionRiesgoActivosUsuario(int idUsuario)
        {
            var validaciones = _context.ValidacionRiesgoActivos
                .Where(x => x.IdDuenno == idUsuario)
                .ToList();

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



        public List<IndicadoresRiesgoEnt> ValidacionRiesgoCompannia()
        {
            var validaciones = _context.ValidacionRiesgoActivos.ToList();

            List<IndicadoresRiesgoEnt> indicadores = new List<IndicadoresRiesgoEnt>();

            foreach (var validacion in validaciones)
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

        public List<ClaseEnt> ResumenClases()
        {
            var listaClases = _context.ResumenActivosClases.ToList();

            List<ClaseEnt> clases = new List<ClaseEnt>();

            foreach (var clase in listaClases)
            {
                clases.Add(new ClaseEnt
                {
                    idClase = clase.IdClase,
                    descripcionClase = clase.DescripcionClase,
                    totalActivos = (int)clase.TotalActivos
                });
            }

            return clases;
        }


        public List<ClaseEnt> ResumenClasesRiesgo()
        {
            var listaRiesgosClase = _context.ValidacionRiesgoClases.ToList();

            List<ClaseEnt> clasesRiesgo = new List<ClaseEnt>();

            foreach (var clase in listaRiesgosClase)
            {
                clasesRiesgo.Add(new ClaseEnt
                {
                    idClase = clase.IdClase,
                    descripcionClase = clase.DescripcionClase,
                    categorizacionRiesgo = clase.ValidacionRiesgo,
                    totalActivos = (int)clase.TotalActivos
                });
            }

            return clasesRiesgo;
        }


         public IndicadoresEnt ObtenerIndicadores(int idUsuario)
          {
             var indicadoresHeader = _context.ESTATUS_ACTIVOS(idUsuario).FirstOrDefault();

              IndicadoresEnt indicadoresOutput = new IndicadoresEnt
              {
                  totalActivos = (int)indicadoresHeader.TOTAL_ACTIVOS,
                  totalInversion = Convert.ToDouble(indicadoresHeader.TOTAL_INVERSION),
                  porcentajeCumplimineto = Convert.ToDouble(indicadoresHeader.PORCENTAJE_CUMPLIMIENTO),
                  totalActivosUsuario = (int)indicadoresHeader.TOTAL_ACTIVOS_USUARIO,
                  activosUsuario = ValidacionRiesgoActivosUsuario(idUsuario),
                  activosCompannia = ValidacionRiesgoCompannia(),
                  resumenClases = ResumenClases(),
                  resumenClasesRiesgo = ResumenClasesRiesgo()
              };

              return indicadoresOutput;
          }
        

       


    }






}



