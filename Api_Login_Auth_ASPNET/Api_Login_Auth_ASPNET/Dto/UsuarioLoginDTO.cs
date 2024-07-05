using System.ComponentModel.DataAnnotations;

namespace Api_Login_Auth_ASPNET.Dto
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "O campo email é obrigatorio"), EmailAddress(ErrorMessage = "Email inválido")]
        public string Email {get; set;}

        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        public string Senha {get; set;}

    }
}
