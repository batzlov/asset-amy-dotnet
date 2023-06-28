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
using SendGrid;
using SendGrid.Helpers.Mail;

namespace asset_amy.Controllers;

[ApiController]
public class ApiAuthController : ControllerBase
{
    private readonly ILogger<ApiAuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ISendGridClient _sendGridClient;
    private readonly UserManager _userManager;

    public ApiAuthController(
        ILogger<ApiAuthController> logger,
        IConfiguration configuration,
        UserManager userManager,
        ISendGridClient sendGridClient
    )
    {
        _logger = logger;
        _configuration = configuration;
        _userManager = userManager;
        _sendGridClient = sendGridClient;
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

    [HttpPut]
    [Route("api/password-forgotten")]
    public async Task<IActionResult> PasswordForgotten(PasswordForgottenDto dto)
    {
        var user = _userManager.GetByEmail(dto.email);

        if(user != null) {   
            user.passwordResetHash = Guid.NewGuid().ToString();
            _userManager.Update(user);

            var fromMail = _configuration.GetSection("EmailSettings:FromMail").Value!;
            var fromName = _configuration.GetSection("EmailSettings:FromName").Value!;

            var msg = new SendGridMessage() {
                From = new EmailAddress(fromMail, fromName),
                Subject = "Passwort zurücksetzen",
                PlainTextContent = "Passwort zurücksetzen",
                HtmlContent = PasswordForgottenMailTemplate(user.firstName, "http://localhost:5220/password-reset/" + user.passwordResetHash)
            };

            msg.AddTo(dto.email);

            await _sendGridClient.SendEmailAsync(msg);
        }
        
        return Ok(new { ok = true });
    }

    [HttpPut]
    [Route("api/password-reset/{passwordResetHash}")]
    public IActionResult PasswordReset(string passwordResetHash, PasswordResetDto dto)
    {
        var user = _userManager.GetByPasswordResetHash(passwordResetHash);

        if(user == null) {
            return BadRequest(new { ok = false, message = "Etwas scheint schief gelaufen zu sein." });
        }

        user.password = BCrypt.Net.BCrypt.HashPassword(dto.password);
        user.passwordResetHash = null;
        _userManager.Update(user);

        return Ok();
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

    private string PasswordForgottenMailTemplate(string userName, string resetLink) {
        return @"
            <html>
                <body>
                    <p>Hallo {{userName}},</p>
                    <p>hier ist dein Link zum Zurücksetzen deines Passworts: <a href='{{resetLink}}'>klick mich</a></p>
                    <p>Wenn du dein Passwort nicht zurücksetzen möchtest, bzw. diese E-Mail auch nicht angefordert hast, bitten wir dich mit dem Support Kontakt aufzunehmen.</p>
                    <p>Dein Asset Amy-Team</p>
                </body>
            </html>
        ".Replace("{{userName}}", userName).Replace("{{resetLink}}", resetLink);
    } 
}