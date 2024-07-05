using Api_Login_Auth_ASPNET.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api_Login_Auth_ASPNET.Services.SenhaService
{
    public class SenhaService : ISenhaInterface
    {
        public IConfiguration _config;

        // fazendo a injeção de dependencia para acessar o token cadastado la no appsettings.json
        public SenhaService(IConfiguration config) 
        {
            this._config = config;
        }


        // metodo que cria a senha hash recebi uma senha normal e retorna senhaHash e senhaSalt
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            
            using (var hmac = new HMACSHA256())
            {
                // criando as senha salt(chave para criar e descriptografar a senha hash) e hash(senha criptografada)
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

            }
        }


        // método que verifica se senha hash já existe no banco
        public bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA256(senhaSalt))
            {
                /* criando a senha hash baseado na senha passada então por logica
                   essa senha hash teria que já ter no banco uma igual*/
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

                /* retorna se computedHash(senha passa por login hasheada) é igual
                   a senhaHash(senha hasheada dentro do banco)*/
                return computedHash.SequenceEqual(senhaHash);
            }
        }



        // método que cria token 
        public string CriarToken(UsuarioModel usuario)
        {
            List<Claim> clainsCriada = new List<Claim>();
            {
                new Claim("Cargo", usuario.Cargo.ToString());
                new Claim("Email", usuario.Email);
                new Claim("Username", usuario.Usuario);
            }

            // criando o token baseado na chave adicionada em appsettings.json
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            // criando a credencial baseado na key acima que foi baseado no token com o método de criptografia
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // agora de fato criando nosso token
            var token = new JwtSecurityToken(

                claims: clainsCriada, // claim criada
                expires: DateTime.Now.AddDays(1), // token expira com 1 ano
                signingCredentials: cred // credencial criada acima
              
                );

            //
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
            
        }
    }


    
}
