using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _dbContext;

        public AdminController(ASSET_MANAGEMENTContext dbContext)
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
            _dbContext.Database.ExecuteSqlInterpolated($"EXEC AgregarUsuario {newUser.nombre}, {newUser.correo}, {newUser.contraseña}, {newUser.idRole}");
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
            _dbContext.Database.ExecuteSqlInterpolated($"EXEC EditarUsuario {id}, {updatedUser.nombre}, {updatedUser.correo}, {updatedUser.contraseña}, {updatedUser.idRole}");
            return Ok("Usuario actualizado exitosamente");
        }
    }
}
