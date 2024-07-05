
using Api_Login_Auth_ASPNET.Dto;
using Api_Login_Auth_ASPNET.Services.AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace Api_Login_Auth_ASPNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // criando a injeção de dependencia para acessar os metodos de authInterface
        private readonly IAuthInterface _authInterface;
        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }



        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLoginDTO usuarioLogin)
        {
            
            var resposta = await _authInterface.Login(usuarioLogin);

            return Ok(resposta);
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(UsuarioCriacaoDTO usuarioCriacao)
        {
            var resposta = await _authInterface.Registrar(usuarioCriacao);

            return Ok(resposta);
        }
    }
}
