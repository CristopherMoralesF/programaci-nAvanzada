using assetManagement.Models;
using assetManagementClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly YourDbContext _dbContext;

        public AdminController(YourDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetActiveUsers()
        {
            var activeUsers = _dbContext.Usuarios.FromSqlRaw("EXEC ObtenerUsuariosActivos").ToList();
            return Ok(activeUsers);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UsuariosEnt newUser)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"EXEC AgregarUsuario {newUser.NOMBRE}, {newUser.CORREO}, {newUser.CONTRASENNA}, {newUser.ID_ROLE}");
            return Ok("Usuario agregado exitosamente");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"EXEC EliminarUsuario {id}");
            return Ok("Usuario eliminado exitosamente");
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, [FromBody] UsuariosEnt updatedUser)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"EXEC EditarUsuario {id}, {updatedUser.NOMBRE}, {updatedUser.CORREO}, {updatedUser.CONTRASENNA}, {updatedUser.ID_ROLE}");
            return Ok("Usuario actualizado exitosamente");
        }
    }
}
