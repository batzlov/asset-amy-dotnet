using System.ComponentModel.DataAnnotations;

namespace asset_amy.Models
{
    public class SignUpForm
    {
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [Compare("Password", ErrorMessage = "Compare")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Required true")]
        public bool PrivacyPolicyAccepted { get; set; }
    }
}