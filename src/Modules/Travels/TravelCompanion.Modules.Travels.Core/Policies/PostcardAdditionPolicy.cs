using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

namespace TravelCompanion.Modules.Travels.Core.Policies;

internal sealed class PostcardAdditionPolicy : IPostcardAdditionPolicy
{
    public async Task<bool> IsOwnerOrTravelParticipant(Guid userId, Travel travel)
    {
        return travel.OwnerId == userId || travel.ParticipantIds.Contains(userId);
    }

    //TODO refactor repeating code?
    public async Task<bool> IsUserTravelOwner(Guid userId, Travel travel)
    {
        return travel.OwnerId == userId;
    }

    public async Task<bool> IsUserTravelParticipant(Guid userId, Travel travel)
    {
        return travel.ParticipantIds.Contains(userId);
    }
}