using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

            var response = await _httpClient.PostAsync("api/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Successful login, redirect to a dashboard or other page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Failed login, display an error message in TempData
                TempData["ErrorMessage"] = "Datos incorrectos, intentelo de nuevo o contacte a su administrador";
                return View();
            }
        }
    }
}
