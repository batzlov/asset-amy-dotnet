using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using asset_amy.Models;
using asset_amy.Managers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace asset_amy.Controllers;

[ApiController, Authorize]
public class ApiExpenseController : ControllerBase 
{
    private readonly ILogger<ApiExpenseController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ExpenseManager _expenseManager;

    public ApiExpenseController(
        ILogger<ApiExpenseController> logger,
        IConfiguration configuration,
        ExpenseManager expenseManager
    )
    {
        _logger = logger;
        _configuration = configuration;
        _expenseManager = expenseManager;
    }

    [HttpPost]
    [Route("api/expenses")]
    public IActionResult CreateExpense(CreateExpenseDto dto)
    {
        if (!ModelState.IsValid) {
            return BadRequest(new { ok = false, message = "Deine Angaben sind fehlerhaft." });
        }

        var expense = new Expense();
        expense.name = dto.name;
        expense.value = dto.value!.Value;

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        expense.belongsToId = int.Parse(userId!);

        _expenseManager.Create(expense);

        return Ok(expense);
    }

    [HttpGet]
    [Route("api/expenses")]
    public IActionResult GetExpenses()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        _logger.LogInformation($"User {userId} requested expenses.");
        var expenses = _expenseManager.GetAllForUser(userId);

        return Ok(expenses);
    }

    [HttpGet]
    [Route("api/expenses/{id}")]
    public IActionResult GetExpense(int id)
    {
        var expense = _expenseManager.GetById(id);

        return Ok(expense);
    }

    [HttpPut]
    [Route("api/expenses/{id}")]
    public IActionResult UpdateExpense(int id, UpdateExpenseDto dto)
    {
        var expense = _expenseManager.GetById(id);
        if(expense == null) {
            return NotFound(new { ok = false, message = "Die Ausgabe konnte nicht gefunden werden." });
        }

        expense.name = dto.name;
        expense.value = dto.value.Value;

        _expenseManager.Update(expense);

        return Ok(expense);
    }

    [HttpDelete]
    [Route("api/expenses/{id}")]
    public IActionResult DeleteExpense(int id)
    {
        var expense = _expenseManager.GetById(id);
        if(expense == null) {
            return NotFound(new { ok = false, message = "Die Ausgabe konnte nicht gefunden werden." });
        }

        _expenseManager.Delete(expense);

        return Ok(expense);
    }
}