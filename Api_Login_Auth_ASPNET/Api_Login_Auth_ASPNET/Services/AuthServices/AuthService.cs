using Api_Login_Auth_ASPNET.Data;
using Api_Login_Auth_ASPNET.Dto;
using Api_Login_Auth_ASPNET.Models;
using Api_Login_Auth_ASPNET.Services.SenhaService;
using Microsoft.EntityFrameworkCore;

namespace Api_Login_Auth_ASPNET.Services.AuthServices
{
    public class AuthService : IAuthInterface
    {
        // criando variavel privada para fazer injeção de dependencia e acessar o banco de dados atravez do _context
        private readonly AppDbContext _context;

        // variavel que acessa o ISenhaservice para pegar o metodo que cria a criptografia
        private readonly ISenhaInterface _senhaInterface;

        public AuthService(AppDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
        }

        
        // Método que registra usuario no banco
        public async Task<Response<UsuarioCriacaoDTO>> Registrar(UsuarioCriacaoDTO usuarioRegistro)
        {
            // criando objeto com resposta padrão 
            Response<UsuarioCriacaoDTO> respostaService = new Response<UsuarioCriacaoDTO>();

            try
            {
                // validando se usuario já existe
                if(!VerificaSeEmailEUsuarioJaExistem(usuarioRegistro))
                {
                    respostaService.Dados = null;
                    respostaService.Status = false;
                    respostaService.Mensagem = "Email/Usuario já cadastrados";
                    return respostaService;
                }

                // depois de validar criando a criptografia atravez do metodo CriarSenhaHash
                _senhaInterface.CriarSenhaHash(usuarioRegistro.Senha, out byte[] senhaHash, out byte[] senhaSalt);


                // criando o usuario com os dados passdos + as senhas criptografadas(hash e salt)
                var usuario = new UsuarioModel()
                {
                    Usuario = usuarioRegistro.Usuario,
                    Email = usuarioRegistro.Email,
                    Cargo = usuarioRegistro.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };


                // adicionando os dados do usuario no banco
                _context.Add(usuario);

                // salvando no banco
                await _context.SaveChangesAsync();

                respostaService.Mensagem = "Usuário criado com sucesso!";
            }
            catch (Exception erro)
            {

                respostaService.Dados = null;
                respostaService.Mensagem = erro.Message;
                respostaService.Status = false;
            }

            return respostaService;
        }


        // método que loga usuario 
        public async Task<Response<string>> Login(UsuarioLoginDTO usuarioLogin)
        {
            // criando objeto com resposta padrão 
            Response<string> respostaService = new Response<string>();

            try
            {
                // verificando usuario passado pelo parametro existe no banco atravez do email
                var usuario = await _context.Usuario.FirstOrDefaultAsync(userBanco => userBanco.Email == usuarioLogin.Email);

                // validando se usuario existe e ja enviando resposta negativa
                if(usuario == null)
                {
                    respostaService.Mensagem = "Credenciais inválidas";
                    respostaService.Status = false;
                    return respostaService;
                }


                // validando as senhas passadas
                // se for false ou seja o metodo VerificaSenhaHash retornou um false(senhas não batem com user do banco)
                if(!_senhaInterface.VerificaSenhaHash(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    respostaService.Mensagem = "Credenciais inválidas";
                    respostaService.Status = false;
                    return respostaService;
                }

                // deu certo usuario existe e senhas estão batendo com o do banco agora gerar o token baseado
                // nos dados de usuario
                var token = _senhaInterface.CriarToken(usuario);


                respostaService.Dados = token;
                respostaService.Mensagem = "Usuário logado com sucesso";



            }
            catch (Exception erro)
            {

                respostaService.Dados = null;
                respostaService.Mensagem = erro.Message;
                respostaService.Status = false;
            }

            return respostaService;
        }





        // metodo que verifica se usuario ja exite no banco de dados
        public bool VerificaSeEmailEUsuarioJaExistem(UsuarioCriacaoDTO usuarioRegistro)
        {
            // verificando se existe ja no banco email e name 
            var usuario = _context.Usuario.FirstOrDefault(
                userBanco => userBanco.Email == usuarioRegistro.Email || 
                userBanco.Usuario == usuarioRegistro.Usuario
                );

            if(usuario != null)
            {
                return false;
            }

            return true;


        }
        
    }
}
