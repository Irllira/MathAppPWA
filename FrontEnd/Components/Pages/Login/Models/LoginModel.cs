using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Components.Pages.Login.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Podaj nazwę użytkownika")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj hasło")]
        public string? Password { get; set; }

    }
}
