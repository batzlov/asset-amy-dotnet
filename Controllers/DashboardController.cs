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
    private readonly RevenueManager _revenueManager;
    private readonly AssetManager _assetManager;

    public DashboardController(
        ILogger<DashboardController> logger,
        ExpenseManager expenseManager,
        RevenueManager revenueManager,
        AssetManager assetManager
    )
    {
        _logger = logger;
        _expenseManager = expenseManager;
        _revenueManager = revenueManager;
        _assetManager = assetManager;
    }

    [Route("dashboard")]
    public IActionResult Index()
    {
        var userId = getCurrentUserId();

        var expenses = _expenseManager.GetAllForUser(userId);
        ViewBag.expensesTotal = formatCurrency(expenses.Sum(e => e.value));
        ViewBag.expensesJson = JsonConvert.SerializeObject(expenses);

        var revenues = _revenueManager.GetAllForUser(userId);
        ViewBag.revenuesTotal = formatCurrency(revenues.Sum(r => r.value));
        ViewBag.revenuesJson = JsonConvert.SerializeObject(revenues);

        var assets = _assetManager.GetAllForUser(userId);
        ViewBag.assetsTotal = formatCurrency(assets.Sum(a => a.value));
        ViewBag.assetsJson = JsonConvert.SerializeObject(assets);

        return View();
    }

    [Route("dashboard/expenses")]
    public IActionResult Expenses()
    {
        var userId = getCurrentUserId();

        var expenses = _expenseManager.GetAllForUser(userId);
        ViewBag.expenses = expenses;
        ViewBag.expensesTotal = expenses.Sum(e => e.value);
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

    private int getCurrentUserId() {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    private string formatCurrency(double value) {
        return String.Format("{0:0.00}", value); 
    }
}
