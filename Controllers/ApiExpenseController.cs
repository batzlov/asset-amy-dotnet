using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using asset_amy.Models;
using asset_amy.Managers;
using Microsoft.AspNetCore.Authorization;

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
        var expense = new Expense();
        expense.name = dto.name;
        expense.value = dto.value;

        // TODO: get currently logged in user
        expense.belongsToId = 1;

        if (!ModelState.IsValid) {
            return BadRequest(new { ok = false, message = "Deine Angaben sind fehlerhaft." });
        }

        _expenseManager.Create(expense);

        return Ok(expense);
    }

    [HttpGet]
    [Route("api/expenses")]
    public IActionResult GetExpenses()
    {
        var expenses = _expenseManager.GetAllForUser(1);

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
        expense.value = dto.value;

        expense.belongsToId = 1;

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