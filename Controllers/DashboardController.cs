using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using asset_amy.Models;

namespace asset_amy.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger)
    {
        _logger = logger;
    }

    [Route("dashboard")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("dashboard/expenses")]
    public IActionResult Expenses()
    {
        return View();
    }

    [Route("dashboard/revenues")]
    public IActionResult Revenues()
    {
        return View();
    }

    [Route("dashboard/asset-allocation")]
    public IActionResult AssetAllocation()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
