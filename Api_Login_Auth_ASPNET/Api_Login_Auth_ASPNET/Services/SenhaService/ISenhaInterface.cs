using Api_Login_Auth_ASPNET.Models;

namespace Api_Login_Auth_ASPNET.Services.SenhaService
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        
        bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);

        string CriarToken(UsuarioModel usuario);
    }
}
