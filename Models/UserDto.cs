using asset_amy.Models;
using System.ComponentModel.DataAnnotations;

public class SignUpUserDto 
{
    [Required, MinLength(2), MaxLength(100)]
    public string firstName { get; set; }

    [Required, MinLength(2), MaxLength(100)]
    public string lastName { get; set; }

    [Required, EmailAddress]
    public string email { get; set; }

    [Required, MinLength(6), MaxLength(100)]
    public string password { get; set; }
}

public class SignInUserDto 
{
    [Required, EmailAddress]
    public string email { get; set; }
    
    [Required, MinLength(6), MaxLength(100)]
    public string password { get; set; }
}