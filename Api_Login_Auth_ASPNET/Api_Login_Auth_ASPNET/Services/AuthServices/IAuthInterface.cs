using Api_Login_Auth_ASPNET.Dto;
using Api_Login_Auth_ASPNET.Models;

namespace Api_Login_Auth_ASPNET.Services.AuthServices
{
    public interface IAuthInterface
    {
        Task<Response<UsuarioCriacaoDTO>> Registrar(UsuarioCriacaoDTO usuarioRegistro);
        Task<Response<string>> Login(UsuarioLoginDTO usuarioLogin);
    }
}
