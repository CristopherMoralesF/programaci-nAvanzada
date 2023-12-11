using assetManagement.Models;
using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace assetManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _httpClient= new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291/api/"); 
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Principal(loginUsuarioEnt usuario) {

            try
            {

                string jsonBody = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _httpClient.PostAsync("Usuarios/validarUsuario", content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    
                    var contentCall = await response.Content.ReadAsStringAsync();
                    var nuevoUsuario = JsonConvert.DeserializeObject<UsuariosEnt>(contentCall); 
                    

                    HttpContext.Session.SetString("nombreUsuario", nuevoUsuario.nombre.ToString());
                    HttpContext.Session.SetInt32("idRole", nuevoUsuario.idRole);

                    IndicadoresEnt indicadores = cargarIndicadores(); 
                    return View(indicadores);
                }
                else
                {
                    // Handle the case when the API request is not successful
                    ViewBag.mensajeError = "Usuario indicado no existe";
                    return View("Index"); 
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.mensajeError = "Error al ingresar, intente nuevamente";
                return View("Index");

            }



        }

        public IndicadoresEnt cargarIndicadores()
        {

            int idUsuario = (int)HttpContext.Session.GetInt32("idRole"); 

            string url = "indicadores/optenerIndicadores?idUsuario=" + idUsuario.ToString();

            HttpResponseMessage res = _httpClient.GetAsync(url).GetAwaiter().GetResult();

            if (res.IsSuccessStatusCode)
            {
                return res.Content.ReadFromJsonAsync<IndicadoresEnt>().Result;

            }

            return new IndicadoresEnt();

        }

        public ActionResult dashboard()
        {
            try
            {
                IndicadoresEnt resultado = cargarIndicadores();
                return View("Principal", resultado);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar el dashboard, intente nuevamente";
                return View("Index");
            }

        }
    }
}