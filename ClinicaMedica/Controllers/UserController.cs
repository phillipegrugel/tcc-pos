using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMedica.Models;
using ClinicaMedica.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMedica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UserController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]UsuarioModel model)
        {
            var user = await _usuarioService.Autenticacao(model.Login, model.Senha);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválido" });

            var token = TokenService.GenerateToken(user);
            user.Senha = "";
            return new
            {
                usuario = user,
                token = token
            };
        }

        [HttpGet]
        [Route("isMedico")]
        [Authorize]
        public async Task<bool> IsMedico()
        {
            return await _usuarioService.IsMedico(User.Identity.Name);
        }
    }
}