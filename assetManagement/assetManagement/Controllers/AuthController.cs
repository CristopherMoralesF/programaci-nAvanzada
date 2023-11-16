using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using assetManagementClassLibrary.Models;

namespace assetManagment.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["ApiSettings:AssetManagementApiUrl"]);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var request = new
            {
                IN_EMAIL = email,
                IN_CONTRASENNA = password
            };

            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7291/api/login", content);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UsuariosEnt>();

                // Guardar información de sesión
                HttpContext.Session.SetString("NOMBRE", user.NOMBRE);
                HttpContext.Session.SetString("CORREO", user.CORREO);
                HttpContext.Session.SetInt32("ID_ROLE", user.ID_ROLE);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Failed login, display an error message in TempData
                TempData["ErrorMessage"] = "Datos incorrectos, intentelo de nuevo o contacte a su administrador";
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Remove("NOMBRE");
            HttpContext.Session.Remove("CORREO");
            HttpContext.Session.Remove("ID_ROLE");

            return RedirectToAction("Login", "Auth"); // Redirige al método de inicio de sesión
        }

    }
}
