using assetManagementApi;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace assetManagement.Controllers
{
    public class ActivoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ActivoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291/api/");
        }



        [HttpGet]
        public async Task<IActionResult> detalleActivos()
        {
            try
            {
                string url = "Activo/consultarActivos"; 
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var activos = JsonConvert.DeserializeObject<List<AuxiliarEnt>>(content);
                    return View(activos);
                }

                return View(new List<AuxiliarEnt>());

                
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle de activos";
                return View("Index", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> detalleActivo(int idActivo)
        {
            try
            {
                string url = "Activo/consultarActivo?idActivo=" + idActivo.ToString();
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if(response.IsSuccessStatusCode ) {

                    var content = await response.Content.ReadAsStringAsync();
                    var activos = JsonConvert.DeserializeObject<ActivoEnt>(content);
                    return View(activos);
                }


                return View(new ActivoEnt());
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle del activo";
                return View("Index", "Home");
            }

        }

        
        public async Task<IActionResult> editarValidacion(int idActivo, int idValidacion, string valor)
        {

            try
            {
                ValidacionClaseEnt nuevaValidacion = new ValidacionClaseEnt
                {
                    idValidacion = idValidacion,
                    idActivo = idActivo,
                    valor = valor,
                    descripcionValidacion = "validación"
                    
                };

                string url = "Conciliacion/modificarValidacionActivo";

                string jsonBody = JsonConvert.SerializeObject(nuevaValidacion);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if(response.IsSuccessStatusCode )
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    return Json(JsonConvert.DeserializeObject<int>(contentResponse));
                }

                return Json(0);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle del activo";
                return View("Index", "Home");
            }

        }
        
        public async Task<IActionResult> agregarValidacion(int idActivo, int idValidacion, string valor)
        {

            try
            {
                ValidacionClaseEnt nuevaValidacion = new ValidacionClaseEnt
                {
                    idValidacion = idValidacion,
                    idActivo = idActivo,
                    valor = valor,
                    descripcionValidacion = "validación"
                };

                string url = "Conciliacion/agregarValidacionActivo";

                string jsonBody = JsonConvert.SerializeObject(nuevaValidacion);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    return Json(JsonConvert.DeserializeObject<int>(contentResponse));
                }

                return Json(0);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle del activo";
                return View("Index", "Home");
            }

        }
        

        [HttpPost]
        public async Task<ActionResult> CrearActivo(string descripcion, string valorAdquisición, string idClase, string idUbicacion, string idDuenno, string idEstado)
        {

            try
            {
                CrearActivoEnt nuevoActivo = new CrearActivoEnt
                {
                    descripcionActivo = descripcion,
                    valorAdquisicion = Convert.ToDouble(valorAdquisición),
                    fechaAdquiscion = DateTime.Now,
                    idClase = Convert.ToInt32(idClase),
                    idUbicacíon = Convert.ToInt32(idUbicacion),
                    idUsuario = Convert.ToInt32(idDuenno),
                    idEstado = Convert.ToInt32(idEstado), 
                    periodosDepreciados = 0
                };

                string url = "Activo/crearActivo";
                string jsonBody = JsonConvert.SerializeObject(nuevoActivo);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if(response.IsSuccessStatusCode)
                {
                    return Json(response); 
                }
                return Json(null);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle del activo";
                return View("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> modificarClase(string idActivo, string idNuevaClase)
        {

            try
            {
                ModificarActivoClaseEnt activoModificar = new ModificarActivoClaseEnt
                {
                    idActivo = Convert.ToInt32(idActivo),
                    idClase = Convert.ToInt32(idNuevaClase)
                };

                string url = "Activo/modificarClase";
                string jsonBody = JsonConvert.SerializeObject(activoModificar);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(url, content);

                if(response.IsSuccessStatusCode)
                {
                    return Json(response);
                }
                return Json(null);

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el detalle del activo";
                return View("Index", "Home");
            }

        }
        
    }
}
