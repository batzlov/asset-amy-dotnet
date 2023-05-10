using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// using BCrypt.Net;
using asset_amy.Models;
using asset_amy.Managers;
namespace asset_amy.Controllers;

[ApiController]
public class ApiAuthController : ControllerBase
{
    private readonly ILogger<ApiAuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserManager _userManager;

    public ApiAuthController(
        ILogger<ApiAuthController> logger,
        IConfiguration configuration,
        UserManager userManager
    )
    {
        _logger = logger;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("api/sign-up")]
    public IActionResult SignUp(SignUpUserDto dto)
    {
        if(!ModelState.IsValid) {
            return BadRequest(new { ok = false, message = "Deine Angaben sind fehlerhaft." });
        } else {
            _logger.LogInformation("User is valid");
        }

        if(_userManager.GetByEmail(dto.email) != null)
        {
            return BadRequest(new { ok = false, message = "Diese E-Mail ist bereits mit einem Account bei uns registriert." });
        }

        var user = new User();
        user.firstName = dto.firstName;
        user.lastName = dto.lastName;
        user.email = dto.email;
        user.password = BCrypt.Net.BCrypt.HashPassword(dto.password);

        _userManager.Add(user);

        return Ok(user);
    }

    [HttpPost]
    [Route("api/sign-in")]
    public IActionResult SignIn(SignInUserDto dto)
    {
        var user = _userManager.GetByEmail(dto.email);
        if(user == null) {
            return Unauthorized(new { ok = false, message = "Deine Anmeldedaten scheinen nicht korrekt zu sein." });
        }

        if(!BCrypt.Net.BCrypt.Verify(dto.password, user.password))
        {
            return Unauthorized(new { ok = false, message = "Deine Anmeldedaten scheinen nicht korrekt zu sein." });
        }

        CreateCookie(user);

        return Created("", new { token = CreateJwtToken(user) });
    }

    private void CreateCookie(User user) 
    {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            new Claim(ClaimTypes.Email, user.email),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            IsPersistent = true,
            AllowRefresh = true,
        };

        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
    }

    private string CreateJwtToken(User user)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            new Claim(ClaimTypes.Email, user.email),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!
            )
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}