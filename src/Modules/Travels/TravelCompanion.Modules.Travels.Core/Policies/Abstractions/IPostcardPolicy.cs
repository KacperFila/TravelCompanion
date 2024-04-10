using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

internal interface IPostcardPolicy
{
    bool DoesUserOwnPostcardTravel(Guid userId, Travel travel);
    bool DoesUserParticipateInPostcardTravel(Guid userId, Travel travel);
    bool CanDeletePostcard(Postcard postcard, Travel travel);
}