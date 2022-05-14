using System.Collections.Generic;
using grud_backend.Helpers;
using grud_backend.Models;
using grud_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grud_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private static Global global = new Global();

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("auth", Name = "AuthUser")]
        public IActionResult Authenticate([FromBody] AuthenticateModel authModel)
        {
            var user = _userService.Authenticate(authModel);
            if (user == null)
                return Ok(new {error= new {mensaje = "Credenciales incorrectas", codigo=41}});
                //global.Email();
            return Ok(user.WithoutPassword());
        }

        [HttpGet("roles")]
        public ActionResult<List<Role>> GetRol()
        {
            return _userService.GetRoles();
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users.WithoutPassword());
        }
        [HttpGet("{id}",Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return user.WithoutPassword();
        }

        [HttpGet("byEmail/{email}")]
        public ActionResult<User> GetByEmail(string email)
        {
            var user = _userService.GetByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return user.WithoutPassword();
        }
        [AllowAnonymous]


        [HttpPost]
        public ActionResult<User> Create([FromBody]User user)
        {
            var emailExists = _userService.GetByEmail(user.Email);
            var usernameExists = _userService.GetByUsername(user.Username);
            if (ModelState.IsValid)
            {
                if (emailExists != null) return BadRequest(new {message = "Email Already Exists", error=44});
                if (usernameExists != null) return BadRequest(new {message = "Username Already Exists", error=43});
                _userService.Create(user);
                
                return CreatedAtRoute("GetUser", new {id = user.Id}, user.WithoutPassword());
            }

            return NotFound(new {mensaje = "Datos faltantes", error = 45});
        }

        public bool GetByCredential(string credential)
        {
            User user;
            if (credential.Contains('@'))
            {
                user = _userService.GetByEmail(credential);
            }
            else
            {
                user = _userService.GetByUsername(credential);
            }

            if (user != null)
            {
                return true;
            }

            return false;
        }

        [HttpPut("estatus/{id}")]
        public IActionResult Estatus(string id, Estatus estatus)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            var newUser = user;
            newUser.Estatus = estatus.estatus;
            _userService.Update(id, newUser);
            return Ok(new {message = "Estatus Actualizado con exito", status = estatus.estatus});
        }

        [HttpPut("updatePW/{id}")]
        public IActionResult UpdatePW(string id, [FromBody] SetPwModel pwModel){
            var user = _userService.Get(id);
            user.Password = global.Decrypt(user.Password);
            if(user == null){
                return NotFound();
            }
            if(user.Password != pwModel.oldPassword){
                return Ok(new {error=new {mensaje="Contraseña Incorrecta",codigo=41}});
            }
            var userIn = user;

            userIn.Password = global.Encrypt(pwModel.newPassword);
            _userService.Update(user.Id,userIn);
            return Ok("Contraseña Actualizada");
        }
        [HttpPut("{id}")]
        public ActionResult Update(string id,[FromBody] User userIn)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            userIn.Password = user.Password;
            _userService.Update(user.Id,userIn);
            return Ok("Usuario Actualizado");
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.Delete((user.Id));
            return Ok("Se elimino usuario");
        }

        [HttpPost("afiliar")]
        public ActionResult<ProvCliente> CreateRelacion([FromBody] ProvCliente pc)
        {
            _userService.CreateRelacion(pc);
            return Ok("se creo relacion");
        }

        [HttpGet("afiliacion/c/{id}")]
        public ActionResult<ProvCliente> GetAfiliacionC(string id) 
        {
            var afi = _userService.GetAfiliacionC(id);    
            if(afi == null)
            {
                return Ok();
            }
            return Ok(afi);
        }

        [HttpGet("afiliacion/p/{id}")]
        public ActionResult<ProvCliente> GetAfiliacionP(string id) 
        {
            var afi = _userService.GetAfiliacionP(id);    
            if(afi == null)
            {
                return Ok();
            }
            return Ok(afi);
        }

        [HttpGet("afiliacion")]
        public IEnumerable<ProvCliente> GetAfiliacion() 
        {
            return _userService.GetAllAfiliaciones();
        }

        

    }
}