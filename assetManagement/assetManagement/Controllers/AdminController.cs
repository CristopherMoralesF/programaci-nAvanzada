using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using assetManagementClassLibrary.Models; // Asegúrate de tener acceso a este espacio de nombres

public class AdminController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(configuration["ApiSettings:AssetManagementApiUrl"]);
    }

    // Mostrar la vista de administración de usuarios
    public async Task<IActionResult> Users()
    {
        var response = await _httpClient.GetAsync("api/admin");
        if (response.IsSuccessStatusCode)
        {
            var users = await response.Content.ReadFromJsonAsync<List<UsuariosEnt>>();
            return View(users);
        }
        else
        {
            // Manejar error si no se puede obtener la lista de usuarios
            return View("Error");
        }
    }

    // Añadir nuevo usuario (POST)
    [HttpPost]
    public async Task<IActionResult> AddUser(UsuariosEnt newUser)
    {
        var jsonRequest = JsonSerializer.Serialize(newUser);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/admin", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Users");
        }
        else
        {
            // Manejar error si no se puede añadir el usuario
            return View("Error");
        }
    }

    // Eliminar usuario
    [HttpPost]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/{userId}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Users");
        }
        else
        {
            // Manejar error si no se puede eliminar el usuario
            return View("Error");
        }
    }

    // Editar usuario (POST)
    [HttpPost]
    public async Task<IActionResult> EditUser(UsuariosEnt updatedUser)
    {
        var jsonRequest = JsonSerializer.Serialize(updatedUser);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"api/admin/{updatedUser.ID_USUARIO}", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Users");
        }
        else
        {
            // Manejar error si no se puede editar el usuario
            return View("Error");
        }
    }
}
