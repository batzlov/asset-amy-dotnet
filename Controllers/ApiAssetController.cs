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
[Route("api/assets")]
public class ApiAssetController : ControllerBase 
{
    private readonly ILogger<ApiAssetController> _logger;
    private readonly IConfiguration _configuration;
    private readonly AssetManager _assetManager;

    public ApiAssetController(
        ILogger<ApiAssetController> logger,
        IConfiguration configuration,
        AssetManager assetManager
    )
    {
        _logger = logger;
        _configuration = configuration;
        _assetManager = assetManager;
    }

    [HttpGet]
    public IActionResult GetAssets()
    {
        var userId = getCurrentUserId();

        var assets = _assetManager.GetAllForUser(userId);

        return Ok(assets);
    }

    [HttpGet("{id}")]
    public IActionResult GetAsset(int id)
    {
        var userId = getCurrentUserId();

        var asset = _assetManager.GetById(id);

        if (asset == null || asset.belongsToId != userId) {
            return NotFound(new { ok = false, message = "Vermögenswert nicht gefunden." });
        }

        return Ok(asset);
    }

    [HttpPost]
    public IActionResult CreateAsset(CreateAssetDto dto)
    {
        if (!ModelState.IsValid) {
            return BadRequest(new { ok = false, message = "Deine Angaben sind fehlerhaft." });
        }

        return Ok();
    }

    private int getCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}