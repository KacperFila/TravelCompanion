using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Exceptions;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Modules.Users.Shared.DTO;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.Users.Core.Services;

internal class UsersModuleApi : IUsersModuleApi
{
    private const string activePlanIdClaimKey = "activePlanId";
    private readonly IUserRepository _userRepository;
    private readonly IContext _context;

    public UsersModuleApi(IUserRepository userRepository, IContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }

    public async Task<bool> CheckIfUserExists(Guid userId)
    {
        return await _userRepository.ExistAsync(userId);
    }

    public async Task<List<string>> GetUsersEmails(List<Guid> usersIds)
    {
        return await _userRepository.GetEmails(usersIds);
    }

    public async Task<string> GetUserEmail(Guid userId)
    {
        return await _userRepository.GetEmail(userId);
    }
    public async Task<List<Guid>> GetUsersIdsAsync()
    {
        return await _userRepository.BrowseActiveAsync();
    }

    public async Task<UserInfoDto> GetUserInfo(Guid userId)
    {
        var user = await _userRepository.GetAsync(userId);

        return new UserInfoDto()
        {
            Email = user.Email,
            UserName = user.Email.Split("@")[0]
        };
    }

    
}