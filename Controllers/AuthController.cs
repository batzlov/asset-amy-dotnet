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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
