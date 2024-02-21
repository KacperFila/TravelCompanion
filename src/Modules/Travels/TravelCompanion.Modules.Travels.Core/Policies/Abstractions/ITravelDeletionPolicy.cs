using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

internal interface ITravelDeletionPolicy
{
    Task<bool> CanDeleteAsync(Travel travel);
}