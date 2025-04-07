using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Exceptions;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Modules.Users.Shared.DTO;

namespace TravelCompanion.Modules.Users.Core.Services;

internal class UsersModuleApi : IUsersModuleApi
{
    private readonly IUserRepository _userRepository;

    public UsersModuleApi(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        return await _userRepository.BrowseActiveIdsAsync();
    }

    public async Task<UserInfoDto> GetUserInfo(Guid userId)
    {
        var user = await _userRepository.GetAsync(userId);

        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }

        return new UserInfoDto()
        {
            Email = user.Email,
            UserName = user.Email.Split("@")[0],
            ActivePlanId = user.ActivePlanId,
        };
    }

    public async Task<List<UserInfoDto>> BrowseUsersInfo(List<Guid> usersIds)
    {
        var users = await _userRepository.BrowseAsync(usersIds);
        var userInfoDto = users.Select(x => new UserInfoDto()
        {
            UserId = x.Id,
            Email = x.Email,
            UserName = x.Email.Split("@")[0],
            ActivePlanId = x.ActivePlanId,
        });

        return userInfoDto.ToList();
    }
}