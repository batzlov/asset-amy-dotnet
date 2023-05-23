using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using asset_amy.Models;
using asset_amy.Managers;
using Newtonsoft.Json;

namespace asset_amy.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly ExpenseManager _expenseManager;

    public DashboardController(
        ILogger<DashboardController> logger,
        ExpenseManager expenseManager
    )
    {
        _logger = logger;
        _expenseManager = expenseManager;
    }

    [Route("dashboard")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("dashboard/expenses")]
    public IActionResult Expenses()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var expenses = _expenseManager.GetAllForUser(userId);
        ViewBag.expenses = expenses;
        ViewBag.expensesJson = JsonConvert.SerializeObject(expenses);

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
