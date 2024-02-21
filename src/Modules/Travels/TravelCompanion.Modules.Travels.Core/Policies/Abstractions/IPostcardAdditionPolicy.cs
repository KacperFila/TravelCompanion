using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

internal interface IPostcardAdditionPolicy
{
    Task<bool> IsUserTravelOwner(Guid userId, Travel travel);
    Task<bool> IsUserTravelParticipant(Guid userId, Travel travel);

    Task<bool> IsOwnerOrTravelParticipant(Guid userId, Travel travel);
}