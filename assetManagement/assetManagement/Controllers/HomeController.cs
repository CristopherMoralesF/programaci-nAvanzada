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
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Principal(UsuariosEnt usuario) {

            try
            {

                JsonContent body = JsonContent.Create(usuario); 

                HttpResponseMessage response = await _httpClient.PostAsync("api/Usuarios/validarUsuario", body);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    
                    UsuariosEnt nuevoUsuario = new UsuariosEnt();
                    // Do something with the nuevoUsuario

                    return Ok(nuevoUsuario);
                }
                else
                {
                    // Handle the case when the API request is not successful
                    return BadRequest("API request failed");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }



        }
        

    }
}