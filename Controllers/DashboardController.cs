using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using asset_amy.Models;
using asset_amy.Managers;
using Newtonsoft.Json;
using ClosedXML;
using ClosedXML.Excel;

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

    [Route("dashboard/get-summary-spreadsheet")]
    public IActionResult GetSummarySpreadsheet() 
    {
        var userId = getCurrentUserId();

        // get data we need to export
        var expenses = _expenseManager.GetAllForUser(userId);
        var revenues = _revenueManager.GetAllForUser(userId);
        var assets = _assetManager.GetAllForUser(userId);

        // convert the data so its easy readable for the user
        var expenseTable = expenses.Select(e => new {
            Name = e.name,
            Beschreibung = e.description,
            Wert = e.value
        }).ToList();

        var revenueTable = revenues.Select(r => new {
            Name = r.name,
            Beschreibung = r.description,
            Wert = r.value
        }).ToList();

        var assetTable = assets.Select(a => new {
            Name = a.name,
            Beschreibung = a.description,
            Wert = a.value,
            Kateogrie = a.type
        }).ToList();

        string fileName = "asset-amy-summary.xlsx";

        IXLWorkbook workbook = new XLWorkbook();

        workbook.AddWorksheet("Ausgaben").FirstCell().InsertTable(expenseTable, false);
        workbook.AddWorksheet("Einnahmen").FirstCell().InsertTable(revenueTable, false);
        workbook.AddWorksheet("Assets").FirstCell().InsertTable(assetTable, false);

        // return file
        var stream = new System.IO.MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();

        return File(
            content,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName
        );
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
        var userId = getCurrentUserId();

        var revenues = _revenueManager.GetAllForUser(userId);
        ViewBag.revenues = revenues;
        ViewBag.revenuesTotal = revenues.Sum(r => r.value);
        ViewBag.revenuesJson = JsonConvert.SerializeObject(revenues);

        return View();
    }

    [Route("dashboard/asset-allocation")]
    public IActionResult AssetAllocation()
    {
        var userId = getCurrentUserId();

        var assets = _assetManager.GetAllForUser(userId);
        ViewBag.assets = assets;
        ViewBag.assetsTotal = assets.Sum(a => a.value);
        ViewBag.assetsJson = JsonConvert.SerializeObject(assets);

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
