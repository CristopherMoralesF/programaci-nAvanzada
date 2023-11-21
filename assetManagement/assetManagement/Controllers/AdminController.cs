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
using Microsoft.EntityFrameworkCore;
using assetManagement.Models;

public class AdminController : Controller
{
    private readonly HttpClient _httpClient;

    private readonly YourDbContext _dbContext;

    public AdminController(YourDbContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _dbContext = dbContext;

        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(configuration["ApiSettings:AssetManagementApiUrl"]);
    }
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
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult AddUser()
    {
        var newUser = new UsuariosEnt();
        return View("AgregarUsuario", newUser);
    }

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
            return View("Error");
        }
    }

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
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(int userId)
    {
        var user = await GetUserFromDatabase(userId);

        if (user != null)
        {
            return View("EditarUsuario", user);
        }
        else
        {
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UsuariosEnt updatedUser)
    {
        // Esta acción debería realizar la actualización del usuario en la base de datos
        var existingUser = await _dbContext.Usuarios.FindAsync(updatedUser.ID_USUARIO);

        if (existingUser != null)
        {
            existingUser.NOMBRE = updatedUser.NOMBRE;
            existingUser.CORREO = updatedUser.CORREO;
            existingUser.CONTRASENNA = updatedUser.CONTRASENNA;
            existingUser.ID_ROLE = updatedUser.ID_ROLE;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Users");
        }
        else
        {
            return View("Error");
        }
    }

    private async Task<UsuariosEnt> GetUserFromDatabase(int userId)
    {
        // Lógica para obtener el usuario de la base de datos utilizando Entity Framework
        var user = await _dbContext.Usuarios.FindAsync(userId);
        return user;
    }
}