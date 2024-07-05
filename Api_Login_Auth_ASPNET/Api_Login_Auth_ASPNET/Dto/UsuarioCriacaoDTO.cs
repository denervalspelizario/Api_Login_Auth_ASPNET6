using Api_Login_Auth_ASPNET.Enum;
using System.ComponentModel.DataAnnotations;

namespace Api_Login_Auth_ASPNET.Dto
{
    public class UsuarioCriacaoDTO
    {
        [Required(ErrorMessage ="O campo usuário é obrigatorio")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatorio"), EmailAddress(ErrorMessage ="Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage ="Senhas não coincidem")]
        public string ConfirmeSenha { get; set; }

        [Required(ErrorMessage = "O campo cargo é obrigatorio")]
        public CargoEnum Cargo { get; set; }
    }
}
