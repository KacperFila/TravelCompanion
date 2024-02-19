using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies;

internal interface IDeleteTravelPolicy
{
    Task<bool> CanDeleteAsync(Travel travel);
}