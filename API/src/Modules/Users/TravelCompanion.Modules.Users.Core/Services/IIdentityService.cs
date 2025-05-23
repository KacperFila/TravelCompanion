using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.DTO;
using TravelCompanion.Modules.Users.Shared.DTO;
using TravelCompanion.Shared.Abstractions.Auth;

namespace TravelCompanion.Modules.Users.Core.Services
{
    public interface IIdentityService
    {
        Task<AccountDto> GetAsync(Guid id);
        Task<JsonWebToken> SignInAsync(SignInDto dto);
        Task SignUpAsync(SignUpDto dto);
        Task ActivateAccountAsync(string token);
        Task<List<UserInfoDto>> BrowseActiveUsersAsync();
    }
}