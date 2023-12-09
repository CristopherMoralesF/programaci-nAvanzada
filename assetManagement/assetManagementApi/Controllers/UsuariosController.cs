using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace assetManagementApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ASSET_MANAGEMENTContext _context;


        public UsuariosController(ASSET_MANAGEMENTContext context)
        {
            _context = context;
        }

        [Route("optenerUsuarios")]
        [HttpGet]
        public List<ResumenUsuario> consultarUsuarios()
        {
            var usuarios = _context.ResumenUsuarios.ToList();
            return usuarios; 
        }

        [Route("consultarUsuario")]
        [HttpGet]
        public ResumenUsuario consultarUsuario(int idUsuario) {
            
            var usuario = (from x in _context.ResumenUsuarios where x.IdUsuario == idUsuario select x).FirstOrDefault();

            return usuario; 
        }

        [Route("validarUsuario")]
        [HttpPost]
        public ActionResult<UsuariosEnt> validarUsuario(UsuariosEnt usuario)
        {
            var usuarioDB = (from x in _context.Usuarios 
                           where x.Correo == usuario.correo && 
                           x.Contrasenna == usuario.contraseña && 
                           x.Estado == 1 select x).FirstOrDefault();

            if (usuarioDB != null) {

                UsuariosEnt nuevoUsuario = new UsuariosEnt{
                    nombre = usuarioDB.Nombre,
                    correo = usuarioDB.Correo,  
                    estado = usuarioDB.Estado,
                    idRole= usuarioDB.IdRole,
                    idUsuario = usuarioDB.IdUsuario,
                    estadoContrasenna = usuarioDB.EstadoContrasenna
                };

                return Ok(nuevoUsuario);
            };

            return BadRequest("No user identified"); 

        }

    }
}
