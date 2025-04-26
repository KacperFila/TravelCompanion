using System.Collections.Generic;

namespace TravelCompanion.Shared.Abstractions.Auth
{
    public interface IAuthManager
    {
        JsonWebToken CreateToken(string userId, string role = null, string email = null, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null);
    }
}