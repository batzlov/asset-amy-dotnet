using System.ComponentModel.DataAnnotations;

namespace asset_amy.Models
{
    public class SignInForm
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public string Password { get; set; }
    }
}