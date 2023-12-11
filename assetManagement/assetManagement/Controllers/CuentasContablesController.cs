using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace assetManagement.Controllers
{
    public class CuentasContablesController : Controller
    {

        private readonly HttpClient _httpClient;

        public CuentasContablesController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291/api/");
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                string url = "CuentaContable/optenerCuentas"; 

                HttpResponseMessage response = await _httpClient.GetAsync(url); 

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cuentasContables = JsonConvert.DeserializeObject<List<CuentaContableEnt>>(content);
                    return View(cuentasContables);

                }
                return View(new List<CuentaContableEnt>());
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar los dueños de activos";
                return View("Index", "Home");
            }
        }

        public async Task<IActionResult> consultarCuenta()
        {
            try
            {
                string url = "CuentaContable/optenerCuentas";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cuentasContables = JsonConvert.DeserializeObject<List<CuentaContableEnt>>(content);
                    return Json(cuentasContables);

                }
                return Json(new List<CuentaContableEnt>());
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar lista de cuentas";
                return View("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> CrearCuentaContable(string idCuenta, string descripcionCuenta, int idCategoria, string naturalezaCuenta)
        {
            try
            {
                CuentaContableEnt nuevaCuenta = new CuentaContableEnt
                {
                    idCuenta = idCuenta,
                    descripcionCuenta = descripcionCuenta,
                    categoriaCuenta = new CategoriaCuentaEnt
                    {
                        idCategoria = idCategoria,
                        descripcionCategoria = "descripcion"
                    },
                    totalDebitos = 0,
                    totalCreditos = 0,
                    balance = 0,
                    naturaleza = naturalezaCuenta
                };

                string url = "CuentaContable/crearCuenta";
                string jsonBody = JsonConvert.SerializeObject(nuevaCuenta);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content); 

                if(response.IsSuccessStatusCode)
                {
                    var contentOutput = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<int>(contentOutput);

                    return Json(resultado); 
                }

                return Json(0);

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al crear cuenta contable";
                return View("Index", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> validarCuentaContableClase(string cuentaContable)
        {
            try
            {
                string url = "CuentaContable/validarCuentaContableClase?idCuenta=" + cuentaContable;

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if(response.IsSuccessStatusCode) {

                    var contentOutput = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<bool>(contentOutput);
                    return Json(resultado);
                }
                return Json(false); 
                

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al crear cuenta contable";
                return View("Index", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> consultarCuentaContable(string idCuentaContable)
        {
            try
            {

                string url = "CuentaContable/buscarCuenta?idCuenta=" + idCuentaContable;

                HttpResponseMessage response = await _httpClient.GetAsync(url); 

                if(response.IsSuccessStatusCode)
                {
                    var contentOutput = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<CuentaContableEnt>(contentOutput);
                    if (resultado != null)
                    {
                        return Json("La cuenta indicada ya existe");

                    }
                    else
                    {
                        return Json("Ok");
                    }

                }
                return Json("Ok"); 
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al crear cuenta contable";
                return View("Index", "Home");
            }

        }
    }
}
