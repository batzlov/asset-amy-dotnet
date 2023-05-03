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

    [HttpGet]
    [Route("sign-up")]
    public IActionResult SignUp()
    {
        _logger.LogInformation("Sign Up");

        return View(new SignUpForm());
    }

    [HttpPost]
    [Route("sign-up")]
    public IActionResult SignUp(SignUpForm model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("User signed up");

            var user = new User();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;

            _userManager.Add(user);

            return RedirectToAction("Index", "Home");
        } 
        else 
        {
            _logger.LogInformation("Something is off");
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
