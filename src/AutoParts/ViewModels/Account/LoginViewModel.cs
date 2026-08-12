using System.ComponentModel.DataAnnotations;

namespace AutoParts.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o usuário.")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lembrar de mim")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
