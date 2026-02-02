using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Components.Pages.Login
{
    public class AccountModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj nazwę użytkownika")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj email")]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj hasło")]
        public string? Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Powtórz hasło")]
        public string? Password2 { get; set; }

    }
}
