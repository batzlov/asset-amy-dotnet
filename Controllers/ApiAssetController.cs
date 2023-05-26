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

        var asset = new Asset();
        asset.name = dto.name;
        asset.value = dto.value!.Value;
        asset.belongsToId = getCurrentUserId();

        _assetManager.Create(asset);

        return Ok(asset);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAsset(int id, UpdateAssetDto dto)
    {
        if (!ModelState.IsValid) {
            return BadRequest(new { ok = false, message = "Deine Angaben sind fehlerhaft." });
        }

        var asset = _assetManager.GetById(id);

        if (asset == null || asset.belongsToId != getCurrentUserId()) {
            return NotFound(new { ok = false, message = "Vermögenswert nicht gefunden." });
        }

        asset.name = dto.name;
        asset.value = dto.value!.Value;

        _assetManager.Update(asset);

        return Ok(asset);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAsset(int id)
    {
        var asset = _assetManager.GetById(id);

        if (asset == null || asset.belongsToId != getCurrentUserId()) {
            return NotFound(new { ok = false, message = "Vermögenswert nicht gefunden." });
        }

        _assetManager.Delete(asset);

        return Ok(new { ok = true, message = "Vermögenswert wurde gelöscht." });
    }

    private int getCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}