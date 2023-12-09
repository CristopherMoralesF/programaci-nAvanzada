using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using assetManagement.Models;
using System.Text;
using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.EntityFrameworkCore;

namespace assetManagment.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ASSET_MANAGEMENTContext _dbContext;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ASSET_MANAGEMENTContext dbContext)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["ApiSettings:AssetManagementApiUrl"]);

            _dbContext = dbContext; // Inyecta el contexto de la base de datos
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

                // Obtener el objeto Usuario directamente de la base de datos
                var usuarioEntity = await _dbContext.Usuarios
                    .SingleOrDefaultAsync(u => u.Correo == user.correo); // Ajustar la lógica de búsqueda según la estructura de tu base de datos

                if (usuarioEntity != null)
                {
                    // Guardar información en la sesión
                    HttpContext.Session.SetString("nombre", usuarioEntity.Nombre);
                    HttpContext.Session.SetString("correo", usuarioEntity.Correo);
                    HttpContext.Session.SetInt32("IdRole", usuarioEntity.IdRole);

                    if (usuarioEntity.IdRole == 1)
                    {
                        return RedirectToAction("Users", "Admin");
                    }
                    else if (usuarioEntity.IdRole == 2)
                    {
                        return RedirectToAction("Index", "Asientos");

                    }
                    else if (usuarioEntity.IdRole == 3)
                    {
                        return RedirectToAction("Principal", "Home");

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Datos incorrectos, intentelo de nuevo o contacte a su administrador";
                    return View();
                }
            }
            else
            {
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