using Microsoft.AspNetCore.Mvc;
using Proyecto_API.Entities;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using assetManagementClassLibrary.Models;
using System.Net;
using System.Net.Mail;
using assetManagement.Models;
using assetManagementClassLibrary.assetManagementDbModel;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;


namespace assetManagement.Controllers
{
  //   [OutputCache(NoStore = true, Duration = 0)]
  //   [SessionFilter] 
    public class ClaseController : Controller
    {

        private readonly HttpClient _httpClient;

        private readonly YourDbContext _dbContext;

        public ClaseController(YourDbContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _dbContext = dbContext;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["ApiSettings:AssetManagementApiUrl"]);
        }

        public IActionResult ListarClases()
        {
            try
            {
                var result = modelClases.consultarClase();
                return View(result);
            }
            catch (Exception ex)
            {
                error.reportarError("ListarClases", ex.Message);
                return View("Index");
            }

        }

        [HttpGet]
        public IActionResult optenerClases()
        {
            try
            {
                var result = modelClases.consultarClase();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("optenerClases", ex.Message);
                return View("Index");
            }

        }

        [HttpGet]
        public IActionResult buscarInformacionClase(string idClase)
        {
            try
            {
                var result = modelClases.buscarInformacionClase(Convert.ToInt16(idClase));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("buscarInformacionClase", ex.Message);
                return View("Index");
            }

        }


        [HttpGet]
        public IActionResult EjecutarDepreciacion()
        {
            try
            {
                ViewBag.listaClases = modelClases.seleccionarClase();
                return View();
            }
            catch (Exception ex)
            {
                error.reportarError("EjecutarDepreciacion", ex.Message);
                return View("Index");
            }

        }

        [HttpPost]
        public IActionResult EjecutarDepreciacion(int idClase, string descripcionAsiento)
        {
            try
            {
                var resultado = modelClases.ejecutarDepreciacion(idClase, descripcionAsiento);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("EjecutarDepreciacion", ex.Message);
                return View("Index");
            }

        }

        public IActionResult crearClase(string clase, int vidaUtil, string activo, string gasto, string depAc)
        {
            try
            {
                ClaseEnt nuevaClase = new ClaseEnt();

                nuevaClase.descripcionClase = clase;
                nuevaClase.vidaUtil = vidaUtil;
                nuevaClase.cuentaActivo = new CuentaContableEnt { idCuenta = activo };
                nuevaClase.cuentaGasto = new CuentaContableEnt { idCuenta = gasto };
                nuevaClase.cuentaDepAcumulada = new CuentaContableEnt { idCuenta = depAc };


                var resultado = modelClases.crearClase(nuevaClase);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("crearClase", ex.Message);
                return View("Index");
            }

        }

        [HttpPost]
        public IActionResult crearValidacionClase(string idClase, string descripcionValidacion)
        {
            try
            {
                ClaseEnt nuevaValidacion = new ClaseEnt
                {
                    idClase = Convert.ToInt32(idClase),
                    validacionClase = new ValidacionClaseEnt { descripcionValidacion = descripcionValidacion }
                };

                var resultado = modelClases.crearValidacionClase(nuevaValidacion);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("crearValidacionClase", ex.Message);
                return View("Index");
            }

        }
    }
}