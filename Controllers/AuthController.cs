using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asset_amy.Models;
using asset_amy.Managers;

namespace asset_amy.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager _userManager;

    public AuthController(
        ILogger<AuthController> logger,
        UserManager userManager
    )
    {
        _logger = logger;
        _userManager = userManager;
    }

    [Route("sign-up")]
    public IActionResult SignUp()
    {
        return View();
    }

    [Route("sign-in")]
    public IActionResult SignIn()
    {
        return View();
    }

    [Route("sign-out")]
    public IActionResult SignOut()
    {
        foreach (var cookie in Request.Cookies.Keys)
        {
            Response.Cookies.Delete(cookie);
        }

        return RedirectToAction("Index", "Home");
    }

    [Route("password-forgotten")]
    public IActionResult PasswordForgotten()
    {
        return View();
    }

    [Route("password-reset/{passwordResetHash}")]
    public IActionResult PasswordReset(string passwordResetHash)
    {
        var user = _userManager.GetByPasswordResetHash(passwordResetHash);

        ViewBag.passwordResetHash = passwordResetHash;

        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [Route("verify-email/{activationHash}")]
    public IActionResult VerifyEmail(string activationHash)
    {
        var user = _userManager.GetByActivationHash(activationHash);

        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }

        user.activationHash = null;
        _userManager.Update(user);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
