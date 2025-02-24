using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualCardAPI.Model;

[ApiController]
[Route("api/wallet")]
[Authorize] // Requires authentication
public class WalletController : ControllerBase
{
    private readonly WalletService _walletService;
    private readonly UserManager<ApplicationUser> _userManager;

    public WalletController(WalletService walletService, UserManager<ApplicationUser> userManager)
    {
        _walletService = walletService;
        _userManager = userManager;
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var balance = await _walletService.GetBalance(userId);
        return Ok(new { balance });
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] Transaction model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _walletService.Deposit(userId, model.Amount);
        return Ok(new { message = "Deposit successful!" });
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] Transaction model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _walletService.Withdraw(userId, model.Amount);

        if (!success) return BadRequest(new { message = "Insufficient balance" });

        return Ok(new { message = "Withdrawal successful!" });
    }
}
