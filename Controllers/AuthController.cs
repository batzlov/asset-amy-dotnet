using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asset_amy.Models;

namespace asset_amy.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("sign-up")]
    public IActionResult SignUp()
    {
        return View(new SignUpForm());
    }

    [HttpPost]
    [Route("sign-up")]
    public IActionResult SignUp(SignUpForm model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("User signed up");
            return RedirectToAction("Index", "Home");
        } 
        else 
        {
            return View(model);
        }
    }

    [Route("sign-in")]
    public IActionResult SignIn()
    {
        return View(new SignInForm());
    }

    [HttpPost]
    [Route("sign-in")]
    public IActionResult SignIn(SignInForm model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("User signed in");
            return RedirectToAction("Index", "Home");
        } 
        else 
        {
            return View(new SignInForm());
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
