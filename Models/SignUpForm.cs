using System.ComponentModel.DataAnnotations;

namespace asset_amy.Models
{
    public class SignUpForm
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Compare("Password", ErrorMessage = "Passworter stimmen nicht überein")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Required true")]
        public bool PrivacyPolicyAccepted { get; set; }
    }
}