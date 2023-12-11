using assetManagementClassLibrary;
using assetManagementClassLibrary.assetManagementDbModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;

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
        public List<UsuariosEnt> consultarUsuarios()
        {
            var usuarios = _context.ResumenUsuarios.ToList();

            List<UsuariosEnt> usuariosOutput = new List<UsuariosEnt>();

            foreach (var usuario in usuarios)
            {
                usuariosOutput.Add(new UsuariosEnt
                {
                    idUsuario = usuario.IdUsuario,
                    nombre = usuario.Nombre,
                    correo = usuario.Correo,
                    role = usuario.NombreRole,
                    idRole = usuario.IdRole,
                    estado = usuario.Estado
                });
            }

            return usuariosOutput; 
        }

        [Route("consultarUsuario")]
        [HttpGet]
        public UsuariosEnt consultarUsuario(int idUsuario) {
            
            var usuario = (from x in _context.ResumenUsuarios where x.IdUsuario == idUsuario select x).FirstOrDefault();

            UsuariosEnt usuarioOutput = new UsuariosEnt
            {
                idUsuario = usuario.IdUsuario,
                nombre = usuario.Nombre,
                correo = usuario.Correo,
                role = usuario.NombreRole,
                idRole = usuario.IdRole,
                estado = usuario.Estado

            };

            return usuarioOutput; 
        }

        [Route("validarUsuario")]
        [HttpPost]
        public ActionResult<UsuariosEnt> validarUsuario(loginUsuarioEnt usuario)
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

        
        [Route("crearUsurio")]
        [HttpPost]
        public int crearUsuario(CrearUsuarioEnt usuario)
        {
            Random rnd = new Random();
            Usuario nuevoUsuario = new Usuario
            {
                Correo = usuario.correo,
                Nombre = usuario.nombre,
                IdRole = usuario.idRole,
                Contrasenna = usuario.correo.Substring(0, 3) + rnd.Next(1, 500).ToString(),
                Estado = 1,
                EstadoContrasenna = 1
            };

            _context.Add(nuevoUsuario);

            return _context.SaveChanges(); 

        }
        

        [Route("buscarCorreo")]
        [HttpGet]
        public string buscarCorreo(string correoElectronico)
        {
            var resultado = (from x in _context.Usuarios
                            where x.Correo == correoElectronico
                            select x).FirstOrDefault();

            if (resultado == null)
            {
                return string.Empty;
            }
            else
            {
                return "El correo selecionado ya existe";
            }
        }

        [Route("desactivarUsuario")]
        [HttpGet]
        public int desactivarUsuario(int idUsuairo)
        {
            _context.Database.ExecuteSqlRaw("EXEC DESACTIVAR_USUARIO {0}", idUsuairo);
            return _context.SaveChanges(); 
        }

        

        [Route("activarUsuario")]
        [HttpGet]
        public int activarUsuario(int idUsuario)
        {
            _context.Database.ExecuteSqlRaw("EXEC ACTIVAR_USUARIO {0}", idUsuario);
            return _context.SaveChanges();
        }

        
        [Route("actualizarUsuario")]
        [HttpPost]
        public int actualizarUsuario(ActualizarUsuarioEnt usuarioActualizar)
        {
            Usuario usuarioDB = (from x in _context.Usuarios
                                 where x.IdUsuario == usuarioActualizar.idUsuario
                                 select x).FirstOrDefault();


            usuarioDB.Nombre = usuarioActualizar.nombre;
            usuarioDB.IdRole = (int)usuarioActualizar.idRole;

            return _context.SaveChanges(); 
        }
        
        

    }
}
