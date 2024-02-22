using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

internal interface ITravelPolicy
{
    Task<bool> CanDeleteAsync(Travel travel);
}