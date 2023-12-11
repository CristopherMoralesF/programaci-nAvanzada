using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace assetManagement.Controllers
{
    public class ClaseController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClaseController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291/api/");
        }

        [HttpGet]
        public async Task<IActionResult> optenerClases()
        {
            try
            {

                string url = "Clases/obtenerListaClases";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaClases = JsonConvert.DeserializeObject<List<ClaseEnt>>(content);
                    return Json(listaClases);

                }

                return Json(null);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle del activo";
                return View("Index", "Home");
            }

        }

        public async Task<ActionResult> ListarClases()
        {
            try
            {
                string url = "Clases/obtenerListaClases";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaClases = JsonConvert.DeserializeObject<List<ClaseEnt>>(content);
                    return View(listaClases);
                }
                return View(new List<ClaseEnt>());
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle de activos";
                return View("Index", "Home");
            }

        }


        [HttpGet]
        public async Task<ActionResult> buscarInformacionClase(string idClase)
        {
            try
            {
                string url = "Clases/consultarClase?idClase=" + idClase;

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaClases = JsonConvert.DeserializeObject<ClaseEnt>(content);
                    return Json(listaClases);
                }
                return Json(null);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle de activos";
                return View("Index", "Home");
            }

        }

        [HttpGet]
        public IActionResult EjecutarDepreciacion()
        {
            try
            {
                string url = "Clases/obtenerListaClases";
                HttpResponseMessage res = _httpClient.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    //Get the list of provinces. 
                    var listaClases = res.Content.ReadFromJsonAsync<List<ClaseEnt>>().Result;

                    List<SelectListItem> listaDropdown = new List<SelectListItem>();

                    listaDropdown.Add(new SelectListItem
                    {
                        Text = "Seleccione la clase",
                        Value = "0"
                    });

                    foreach (var clase in listaClases)
                    {
                        listaDropdown.Add(new SelectListItem
                        {
                            Text = clase.descripcionClase,
                            Value = clase.idClase.ToString()
                        });
                    }

                    ViewBag.listaClases = listaDropdown;
                    return View();

                }

                ViewBag.mensajeError = "Error al generar la depreciación de clases";
                return View("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar la depreciación de clases";
                return View("Index", "Home");
            }

        }


        [HttpPost]
        public async Task<IActionResult> EjecutarDepreciacion(int idClase, string descripcionAsiento)
        {
            try
            {

                ClaseDepreciacionEnt claseDepreciada = new ClaseDepreciacionEnt
                {
                    idClase = idClase,
                    descripcionClase = descripcionAsiento
                };

                string url = "Clases/ejecutarDepreciacionClase";

                string jsonBody = JsonConvert.SerializeObject(claseDepreciada);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var listaActivosDepreciado = JsonConvert.DeserializeObject<List<AuxiliarEnt>>(contentResponse);

                    return Json(listaActivosDepreciado);
                }
                return Json(null);

            }
            catch (Exception ex)
            {
                return Json(null);
            }

        }

        public async Task<IActionResult> crearClase(string clase, int vidaUtil, string activo, string gasto, string depAc)
        {
            try
            {
                NuevaClaseEnt nuevaClase = new NuevaClaseEnt();

                nuevaClase.descripcionClase = clase;
                nuevaClase.vidaUtil = vidaUtil;
                nuevaClase.cuentaActivo = activo;
                nuevaClase.cuentaGasto = gasto;
                nuevaClase.cuentaDepAcumulada = depAc;

                string url = "Clases/crearClase";

                string jsonBody = JsonConvert.SerializeObject(nuevaClase);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var finalOutput = JsonConvert.DeserializeObject<int>(contentResponse);
                    return Json(finalOutput);
                }
                return Json(0);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar la depreciación de clases";
                return View("Index", "Home");
            }



        }

        [HttpPost]
        public async Task<IActionResult> crearValidacionClase(string idClase, string descripcionValidacion)
        {
            try
            {
                NuevoTipoValidacionEnt nuevaValidacion = new NuevoTipoValidacionEnt
                {
                    IdClase = Convert.ToInt32(idClase),
                    DescripcionValidacion = descripcionValidacion
                };

                string url = "Clases/crearValidacionClase";

                string jsonBody = JsonConvert.SerializeObject(nuevaValidacion);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var finalOutput = JsonConvert.DeserializeObject<int>(contentResponse);
                    return Json(finalOutput);

                }
                return Json(0);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar la depreciación de clases";
                return View("Index", "Home");
            }

        }
    }
}
