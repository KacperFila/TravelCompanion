using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.DTO;
using TravelCompanion.Modules.Users.Core.Services;
using TravelCompanion.Shared.Abstractions.Auth;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Users.Api.Controllers;

internal sealed class AccountController : BaseController
{
    private readonly IIdentityService _identityService;
    private readonly IContext _context;

    public AccountController(IIdentityService identityService, IContext context)
    {
        _identityService = identityService;
        _context = context;
    }

    [HttpGet("info")]
    [Authorize]
    public async Task<ActionResult<AccountDTO>> GetAsync()
        => OkOrNotFound(await _identityService.GetAsync(_context.Identity.Id));

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUpAsync(SignUpDTO dto)
    {
        await _identityService.SignUpAsync(dto);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JsonWebToken>> SignInAsync(SignInDTO dto)
        => Ok(await _identityService.SignInAsync(dto));

    [HttpGet("activate/{token}")]
    public async Task<ActionResult> ActivateAccountAsync([FromRoute] string token)
    {
        await _identityService.ActivateAccountAsync(token);
        return Ok();
    }
}
