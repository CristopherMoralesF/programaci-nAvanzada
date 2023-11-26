using assetManagement.Models;
using assetManagementClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Proyecto_API.Entities;
using System.Text;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace assetManagement.Controllers
{
    public class ActivoController : Controller
    {
        private readonly HttpClient _httpClient;

        private readonly YourDbContext _dbContext;


        public ActivoController(YourDbContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _dbContext = dbContext;

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["ApiSettings:AssetManagementApiUrl"]);
        }

        [HttpGet]
        public async Task<IActionResult> DetalleActivo(int idActivo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/activo/consultaractivo/{idActivo}");
                if (response.IsSuccessStatusCode)
                {
                    var activo = await response.Content.ReadFromJsonAsync<ActivoEnt>();
                    return View("DetalleActivo", activo);
                }
                else
                {

                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                error.ReportarError("DetalleActivo", ex.Message);
                return View("Index");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarValidacion(int idActivo, int idValidacion, string valor)
        {
            try
            {
                ValidacionClaseEnt nuevaValidacion = new ValidacionClaseEnt
                {
                    idValidacion = idValidacion,
                    idActivo = idActivo,
                    valor = valor
                };

                var jsonRequest = JsonSerializer.Serialize(nuevaValidacion);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/activo/editarvalidacion/{idActivo}/{idValidacion}", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return Json(resultado);
                }
                else
                {
                    return Json(new { error = "Failed to update validation" });
                }
            }
            catch (Exception ex)
            {
                error.ReportarError("EditarValidacion", ex.Message);
                return Json(new { error = "An error occurred while updating validation" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarValidacion(int idActivo, int idValidacion, string valor)
        {
            try
            {
                ValidacionClaseEnt nuevaValidacion = new ValidacionClaseEnt
                {
                    idValidacion = idValidacion,
                    idActivo = idActivo,
                    valor = valor
                };

                var jsonRequest = JsonSerializer.Serialize(nuevaValidacion);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/activo/agregarvalidacion/{idActivo}/{idValidacion}", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return Json(resultado);
                }
                else
                {
                    return Json(new { error = "Failed to add validation" });
                }
            }
            catch (Exception ex)
            {
                error.ReportarError("AgregarValidacion", ex.Message);
                return Json(new { error = "An error occurred while adding validation" });
            }
        }



        [HttpPost]
        public async Task<IActionResult> CrearActivo(string descripcion, string valorAdquisicion, string idClase, string idUbicacion, string idDuenno, string idEstado)
        {
            try
            {
                ActivoEnt nuevoActivo = new ActivoEnt
                {
                    descripcionActivo = descripcion,
                    valorAdquisicion = Convert.ToDouble(valorAdquisicion),
                    FechaAdquisicion = DateTime.Now,
                    claseActivo = new ClaseEnt { idClase = Convert.ToInt32(idClase) },
                    ubicacionActivo = new UbicacionEnt { idUbicacion = Convert.ToInt32(idUbicacion) },
                    duennoActivo = new UsuariosEnt { idUsuario = Convert.ToInt32(idDuenno) },
                    estadoActivo = new EstadoEnt { idEstado = Convert.ToInt32(idEstado) }
                };

                var response = await _httpClient.PostAsJsonAsync("api/activo/crearactivo", nuevoActivo);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return Json(resultado);
                }
                else
                {
                    return Json(new { error = "Failed to create asset" });
                }
            }
            catch (Exception ex)
            {
                error.ReportarError("CrearActivo", ex.Message);
                return Json(new { error = "An error occurred while creating asset" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ModificarClase(string idActivo, string idNuevaClase)
        {
            try
            {
                ActivoEnt activoModificar = new ActivoEnt
                {
                    idActivo = Convert.ToInt32(idActivo),
                    claseActivo = new ClaseEnt { idClase = Convert.ToInt32(idNuevaClase) }
                };

                var resultado = await _httpClient.PostAsJsonAsync("api/activo/modificarclase", activoModificar);

                if (resultado.IsSuccessStatusCode)
                {
                    var responseContent = await resultado.Content.ReadAsStringAsync();
                    return Json(responseContent);
                }
                else
                {
                    return Json(new { error = "Failed to modify asset class" });
                }
            }
            catch (Exception ex)
            {
                error.ReportarError("ModificarClase", ex.Message);
                return Json(new { error = "An error occurred while modifying asset class" });
            }
        }

    }
}
