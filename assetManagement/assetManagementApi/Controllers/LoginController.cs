using assetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace assetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly YourDbContext _dbContext;

        public LoginController(YourDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            var user = _dbContext.Usuarios
    .FromSqlInterpolated($"EXECUTE VALIDAR_USUARIO {model.IN_EMAIL}, {model.IN_CONTRASENNA}")
    .AsEnumerable()
    .FirstOrDefault();


            if (user != null)
            {
                // Successful login, return a success response
                return Ok();
            }
            else
            {
                // Failed login, return a bad request response
                return BadRequest();
            }
        }
    }

    public class LoginRequestModel
    {
        public string IN_EMAIL { get; set; }
        public string IN_CONTRASENNA { get; set; }
    }
}
