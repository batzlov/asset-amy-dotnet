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
[Route("api/revenues")]
public class ApiRevenueController : ControllerBase 
{
    private readonly ILogger<ApiRevenueController> _logger;
    private readonly IConfiguration _configuration;
    private readonly RevenueManager _revenueManager;

    public ApiRevenueController(
        ILogger<ApiRevenueController> logger,
        IConfiguration configuration,
        RevenueManager revenueManager
    )
    {
        _logger = logger;
        _configuration = configuration;
        _revenueManager = revenueManager;
    }

    [HttpGet]
    public IActionResult GetRevenues()
    {
        var userId = getCurrentUserId();

        var revenues = _revenueManager.GetAllForUser(userId);

        return Ok(revenues);
    }

    [HttpGet("{id}")]
    public IActionResult GetRevenue(int id)
    {
        var userId = getCurrentUserId();

        var revenue = _revenueManager.GetById(id);

        if (revenue == null) {
            return NotFound(new { ok = false, message = "Einnahme nicht gefunden." });
        }

        return Ok(revenue);
    }

    [HttpPost]
    public IActionResult CreateRevenue(CreateRevenueDto dto)
    {
        if (!ModelState.IsValid) {
            return BadRequest(new { ok = false, message = "Deine Angaben sind fehlerhaft." });
        }

        var revenue = new Revenue();
        revenue.name = dto.name;
        revenue.value = dto.value!.Value;
        revenue.belongsToId = getCurrentUserId();

        _revenueManager.Create(revenue);

        return Ok(revenue);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRevenue(int id, UpdateRevenueDto dto)
    {
        if (!ModelState.IsValid) {
            return BadRequest(new { ok = false, message = "Deine Angaben sind fehlerhaft." });
        }

        var revenue = _revenueManager.GetById(id);

        if (revenue == null || revenue.belongsToId != getCurrentUserId()) {
            return NotFound(new { ok = false, message = "Einnahme nicht gefunden." });
        }

        revenue.name = dto.name;
        revenue.value = dto.value!.Value;

        _revenueManager.Update(revenue);

        return Ok(revenue);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRevenue(int id)
    {
        var revenue = _revenueManager.GetById(id);

        if (revenue == null || revenue.belongsToId != getCurrentUserId()) {
            return NotFound(new { ok = false, message = "Einnahme nicht gefunden." });
        }

        _revenueManager.Delete(revenue);

        return Ok(new { ok = true, message = "Einnahme gelöscht." });
    }

    private int getCurrentUserId() {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}