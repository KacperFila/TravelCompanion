using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

internal interface IPostcardPolicy
{
    Task<bool> DoesUserOwnPostcardTravel(Guid userId, Travel travel);
    Task<bool> DoesUserParticipateInPostcardTravel(Guid userId, Travel travel);
    Task<bool> CanDeletePostcard(Postcard postcard, Travel travel);
    Task<bool> DoesUserOwnOrParticipateInPostcardTravel(Guid userId, Travel travel);
}