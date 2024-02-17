using System.Threading.Tasks;
using System;
using TravelCompanion.Modules.Users.Core.DTO;
using TravelCompanion.Shared.Abstractions.Auth;

namespace TravelCompanion.Modules.Users.Core.Services
{
    public interface IIdentityService
    {
        Task<AccountDto> GetAsync(Guid id);
        Task<JsonWebToken> SignInAsync(SignInDto dto);
        Task SignUpAsync(SignUpDto dto);
    }
}