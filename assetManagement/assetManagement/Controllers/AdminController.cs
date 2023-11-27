using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Microsoft.EntityFrameworkCore;
using assetManagement.Models;
using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;

public class AdminController : Controller
{
    private readonly HttpClient _httpClient;

    private readonly ASSET_MANAGEMENTContext _dbContext;

    public AdminController(ASSET_MANAGEMENTContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
        var existingUser = await _dbContext.Usuarios.FindAsync(updatedUser.idUsuario);

        if (existingUser != null)
        {
            existingUser.Nombre = updatedUser.nombre;
            existingUser.Correo = updatedUser.correo;
            existingUser.Contrasenna = updatedUser.contraseña;
            existingUser.IdRole = updatedUser.idRole;

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
        // Logic to retrieve the user from the database using Entity Framework
        var dbUser = await _dbContext.Usuarios.FindAsync(userId);

        // Mapping properties from Usuario (dbUser) to UsuariosEnt
        UsuariosEnt user = new UsuariosEnt
        {
            idUsuario = dbUser.IdUsuario,
            nombre=dbUser.Nombre,
            correo = dbUser.Correo,
            contraseña = dbUser.Contrasenna,
            idRole = dbUser.IdRole,
            estado = dbUser.Estado,
            estadoContrasenna = dbUser.EstadoContrasenna,
          
        };

        return user;
    }

}