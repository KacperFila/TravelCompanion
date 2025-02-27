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
    private const string activePlanIdClaimKey = "activePlanId";
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

    public async Task SetUserActivePlan(Guid userId, Guid planId)
    {
        var user = await _userRepository.GetAsync(userId);

        if (user.Claims.TryGetValue(activePlanIdClaimKey, out var _))
        {
            user.Claims[activePlanIdClaimKey] = [planId.ToString()];
        }
        else
        {
            user.Claims.Add(activePlanIdClaimKey, [planId.ToString()]);
        }

        await _userRepository.UpdateAsync(user);
    }

    public async Task<Guid?> GetUserActivePlan(Guid userId)
    {
        var user = await _userRepository.GetAsync(userId);
        Console.WriteLine($"USER: {user}");
        var userActivePlanId = user.Claims[activePlanIdClaimKey].FirstOrDefault();
        Console.WriteLine($"USERACTIVEPLANID: {userActivePlanId}");

        if (userActivePlanId is null) // PLAN MIGHT BE NOT YET CHOSEN
        {
            return null;
        }

        var isUserActivePlanIdValid = Guid.TryParse(userActivePlanId, out var activePlanId);

        if(!isUserActivePlanIdValid)
        {
            throw new InvalidClaimValueException(activePlanIdClaimKey);
        }

        Console.WriteLine($"RETURNED: {activePlanId}");

        return activePlanId;
    }
}