using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [Route("api/optenerUsuarios")]
        [HttpGet]
        public List<ResumenUsuario> consultarUsuarios()
        {
            var usuarios = _context.ResumenUsuarios.ToList();
            return usuarios; 
        }

        [Route("api/consultarUsuario")]
        [HttpGet]
        public ResumenUsuario consultarUsuario(int idUsuario) {
            
            var usuario = (from x in _context.ResumenUsuarios where x.IdUsuario == idUsuario select x).FirstOrDefault();

            return usuario; 
        }

    }
}
