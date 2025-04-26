using System;
using System.Collections.Generic;

namespace TravelCompanion.Shared.Abstractions.Contexts
{
    public interface IIdentityContext
    {
        bool IsAuthenticated { get; }
        public Guid Id { get; }
        string Role { get; }
        string Email { get; }
        Dictionary<string, IEnumerable<string>> Claims { get; }
    }
}