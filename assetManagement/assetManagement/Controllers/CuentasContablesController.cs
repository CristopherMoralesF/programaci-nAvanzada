using assetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Proyecto_API.Entities;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using assetManagementClassLibrary.Models;

namespace assetManagement.Controllers
{
    public class CuentasContablesController : Controller
    {

        private readonly HttpClient _httpClient;

        private readonly YourDbContext _dbContext;

        public CuentasContablesController(YourDbContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _dbContext = dbContext;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["ApiSettings:AssetManagementApiUrl"]);
        }

     
        // GET: CuentasContables
        public IActionResult Index()
        {
            try
            {
                var resultados = cuentasModel.consultarCuentas();
                return View(resultados);
            }
            catch (Exception ex)
            {
                error.reportarError("CuentasContables", ex.Message);
                return View("Index");
            }


        }

        [HttpGet]
        public IActionResult consultarCuentaContable(string idCuentaContable)
        {
            try
            {
                var resultado = cuentasModel.validarCuentaDuplicada(idCuentaContable);

                if (resultado != null)
                {
                    return Json("La cuenta indicada ya existe", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                error.reportarError("consultarCuentaContable", ex.Message);
                return View("Index");
            }

        }

        public IActionResult consultarCuenta()
        {
            try
            {
                var resultado = cuentasModel.consultarCuentas();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("consultarCuenta", ex.Message);
                return View("Index");
            }

        }

        [HttpPost]
        public IActionResult CrearCuentaContable(string idCuenta, string descripcionCuenta, int idCategoria, string naturalezaCuenta)
        {
            try
            {
                CuentaContableEnt nuevaCuenta = new CuentaContableEnt
                {
                    idCuenta = idCuenta,
                    descripcionCuenta = descripcionCuenta,
                    categoriaCuenta = new CategoriaCuentaEnt
                    {
                        idCategoria = idCategoria
                    },
                    totalDebitos = 0,
                    totalCreditos = 0,
                    naturaleza = naturalezaCuenta
                };

                var resultado = cuentasModel.CrearCuentaContable(nuevaCuenta);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("CrearCuentaContable", ex.Message);
                return View("Index");
            }

        }


        [HttpGet]
        public IActionResult validarCuentaContableClase(string cuentaContable)
        {
            try
            {
                Boolean validarCuenta = cuentasModel.validarCuentaContableClase(cuentaContable);
                return Json(validarCuenta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                error.reportarError("validarCuentaContableClase", ex.Message);
                return View("Index");
            }

        }
    }
    }

