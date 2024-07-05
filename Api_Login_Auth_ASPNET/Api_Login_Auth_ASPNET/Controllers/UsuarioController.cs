using Api_Login_Auth_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Login_Auth_ASPNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public ActionResult<Response<string>> Getusuario()
        {
            Response<string> response = new Response<string>();
            response.Mensagem = "Usuario logado com sucesso";

            return response;
        }
    }
}
