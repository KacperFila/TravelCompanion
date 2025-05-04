using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Services;
using TravelCompanion.Modules.Users.Shared.DTO;

namespace TravelCompanion.Modules.Users.Api.Controllers;

internal sealed class UsersController : BaseController
{
    private readonly IIdentityService _identityService;

    public UsersController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet("browse")]
    public async Task<List<UserInfoDto>> GetAllUsers()
    {
        var users = await _identityService.BrowseActiveUsersAsync();
        return await Task.FromResult(users);
    }
}
