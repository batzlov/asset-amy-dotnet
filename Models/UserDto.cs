using asset_amy.Models;

public class SignUpUserDto 
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}

public class SignInUserDto 
{
    public string email { get; set; }
    public string password { get; set; }
}