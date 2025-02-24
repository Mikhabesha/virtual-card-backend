using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualCardAPI.Model;
using VirtualCardAPI.Services;

[ApiController]
[Route("api/cards")]
[Authorize]
public class CardController : ControllerBase
{
    private readonly CardService _cardService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CardController(CardService cardService, UserManager<ApplicationUser> userManager)
    {
        _cardService = cardService;
        _userManager = userManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCard()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var existingCard = await _cardService.GetUserCard(userId);

        if (existingCard != null)
            return BadRequest(new { message = "User already has a card" });

        var card = await _cardService.CreateCard(userId);
        return Ok(card);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetCardDetails()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var card = await _cardService.GetUserCard(userId);

        if (card == null)
            return NotFound(new { message = "Card not found" });

        return Ok(card);
    }

    [HttpPost("topup")]
    public async Task<IActionResult> TopUpCard([FromBody] Transaction model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _cardService.TopUpCard(userId, model.Amount);

        if (!success) return BadRequest(new { message = "Insufficient funds or card not found" });

        return Ok(new { message = "Card funded successfully!" });
    }

    [HttpPost("deactivate")]
    public async Task<IActionResult> DeactivateCard()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _cardService.DeactivateCard(userId);

        if (!success) return NotFound(new { message = "Card not found" });

        return Ok(new { message = "Card deactivated successfully!" });
    }

    [HttpPost("reactivate")]
    public async Task<IActionResult> ReactivateCard()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _cardService.ReactivateCard(userId);

        if (!success) return NotFound(new { message = "Card not found" });

        return Ok(new { message = "Card reactivated successfully!" });
    }


}
