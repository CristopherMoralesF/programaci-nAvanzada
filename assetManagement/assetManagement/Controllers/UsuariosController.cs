using assetManagementClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace assetManagement.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuariosController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7291/api/");
        }

        
        [HttpGet]
        public async Task<ActionResult> ListaUsuarios()
        {
            try
            {
                string url = "Usuarios/optenerUsuarios";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<UsuariosEnt>>(content);
                    return View(usuarios);
                }

                return View(new List<UsuariosEnt>());   

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar los dueños de activos";
                return View("Index", "Home");
            }

        }
        

        [HttpGet]
        public async Task<IActionResult> consultarUsuarios()
        {

            try
            {

                string url = "Usuarios/optenerUsuarios";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<UsuariosEnt>>(content); 

                    return Json(usuarios);

                }
                return Json(null);

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar la lista de usuarios";
                return View("Index", "Home");
            }

        }
        

        [HttpGet]
        public async Task<IActionResult> consultarUsuario(int idUsuario)
        {

            try
            {
                string url = "Usuarios/consultarUsuario?idUsuario=" + idUsuario.ToString();

                HttpResponseMessage response = await _httpClient.GetAsync(url); 

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var usuarioOutput = JsonConvert.DeserializeObject<UsuariosEnt>(content);
                    return Json(usuarioOutput); 
                }
                return Json(new UsuariosEnt()); 
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al generar la información del usuario";
                return View("Index", "Home");
            }

        }

        
        [HttpPost]
        public async Task<IActionResult> actualizarUsuario(int idUsuario, string nombreUsuario, int idRoleUsuario)
        {
            try
            {
                ActualizarUsuarioEnt usuarioActualizar = new ActualizarUsuarioEnt
                {
                    idUsuario = idUsuario,
                    nombre = nombreUsuario,
                    idRole = idRoleUsuario
                };

                string url = "Usuarios/actualizarUsuario";
                string jsonBody = JsonConvert.SerializeObject(usuarioActualizar);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content); 

                if(response.IsSuccessStatusCode)
                {
                    return Json(response); 
                }

                return Json(0);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al actualizar el usuario";
                return View("Index", "Home");
            }

        }
        
        
        [HttpPost]
        public async Task<IActionResult> buscarCorreo(string correo)
        {

            try
            {
                string url = "Usuarios/buscarCorreo?correoElectronico=" + correo;

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Json(JsonConvert.DeserializeObject<string>(content));    
                }

                return Json(0); 

            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al buscar el correo";
                return View("Index", "Home");
            }

        }
        
        
        [HttpPost]
        public async Task<IActionResult> crearUsuario(string correoElectronico, string nombre, int role)
        {
            try
            {
                CrearUsuarioEnt nuevoUsuario = new CrearUsuarioEnt();

                nuevoUsuario.correo = correoElectronico;
                nuevoUsuario.nombre = nombre;
                nuevoUsuario.idRole = role;

                string url = "Usuarios/crearUsurio";

                string jsonBody = JsonConvert.SerializeObject(nuevoUsuario);
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
                ViewBag.mensajeError = "Error al crear un nuevo usuario";
                return View("Index", "Home");
            }

        }
        
        
        
        [HttpPost]
        public async Task<IActionResult> activarUsuario(int idUsuario)
        {

            try
            {
                string url = "Usuarios/activarUsuario?idUsuario=" + idUsuario.ToString(); 
                
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    return Json(JsonConvert.DeserializeObject<int>(contentResponse));
                }
                return Json(0); 
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al activar usuario";
                return View("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> desactivarUsuario(int idUsuario)
        {

            try
            {
                string url = "Usuarios/desactivarUsuario?idUsuairo=" + idUsuario.ToString();

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    return Json(JsonConvert.DeserializeObject<int>(contentResponse));
                }
                return Json(0);
            }
            catch (Exception ex)
            {
                ViewBag.mensajeError = "Error al desactivar usuario";
                return View("Index", "Home");
            }

        }
    }
}
