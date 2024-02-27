using System;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Modules.Users.Shared;

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
}