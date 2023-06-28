using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asset_amy.Models;

public partial class User
{
    public int id { get; set; }

    public string firstName { get; set; } = null!;

    public string lastName { get; set; } = null!;

    public string email { get; set; } = null!;

    public string password { get; set; } = null!;

    public string role { get; set; } = null!;

    public string? activationHash { get; set; }

    public string? passwordResetHash { get; set; }

    public DateTime createdAt { get; set; }

    public DateTime updatedAt { get; set; }

    public virtual ICollection<Asset> assets { get; set; } = new List<Asset>();

    public virtual ICollection<Expense> expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Revenue> revenues { get; set; } = new List<Revenue>();
}

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

public class PasswordForgottenDto 
{
    [Required, EmailAddress]
    public string email { get; set; }
}

public class PasswordResetDto 
{
    [Required, MinLength(6), MaxLength(200)]
    public string password { get; set; }

    [Required, MinLength(6), MaxLength(200)]
    public string confirmPassword { get; set; }
}