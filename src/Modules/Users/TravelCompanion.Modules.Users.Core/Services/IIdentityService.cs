using System.Threading.Tasks;
using System;
using TravelCompanion.Modules.Users.Core.DTO;
using TravelCompanion.Shared.Abstractions.Auth;

namespace TravelCompanion.Modules.Users.Core.Services
{
    public interface IIdentityService
    {
        Task<AccountDTO> GetAsync(Guid id);
        Task<JsonWebToken> SignInAsync(SignInDTO dto);
        Task SignUpAsync(SignUpDTO dto);
        Task ActivateAccountAsync(string token);
    }
}