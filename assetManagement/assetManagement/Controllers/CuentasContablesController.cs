using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace assetManagement.Controllers
{
    public class CuentasContablesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CuentasContablesController()
        {

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/cuentas/optenerCuentas");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cuentas = JsonConvert.DeserializeObject<IEnumerable<CuentaContableEnt>>(content);
                    return View(cuentas);
                }
                else
                {
                    return View(Index);
                }
            }
            catch (Exception)
            {
                return View(Index);
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> consultarCuentaContable(string idCuentaContable)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/cuentas/buscarCuenta?idCuenta=" + idCuentaContable);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cuenta = JsonConvert.DeserializeObject<CuentaContableEnt>(content);

                    return View(cuenta);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<bool> validarCuentaContableClase(string idCuentaContable)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/cuentas/validarCuentaContableClase?idCuenta=" + idCuentaContable);

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<bool>().Result;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        [HttpPost]
        public async Task<IActionResult> CrearCuentaContable(string idCuenta, string descripcionCuenta, int idCategoria, string naturalezaCuenta)//CuentaContableEnt nuevaCuenta)
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


                var response = await _httpClient.PostAsJsonAsync("api/cuentas/crearCuenta", nuevaCuenta);
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return Json(resultado);
                }
                else
                {
                    return Json(new { error = "Error al crear la cuenta" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al crear la cuenta" });
            }


        }

        public async Task<List<CuentaContableEnt>> ConsultarCuenta()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/cuentas/optenerCuentas");

               

                if (response.IsSuccessStatusCode)
                {

                    return response.Content.ReadFromJsonAsync<List<CuentaContableEnt>>().Result;
                }

                return new List<CuentaContableEnt>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}