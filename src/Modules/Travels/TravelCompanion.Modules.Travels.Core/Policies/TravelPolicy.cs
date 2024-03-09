using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

namespace TravelCompanion.Modules.Travels.Core.Policies;

public class TravelPolicy : ITravelPolicy
{
    public async Task<bool> CanDeleteAsync(Travel travel)
    {
        //if (!travel.AllParticipantsPaid)
        //{
        //    return false;
        //}

        return true;
    }

    public async Task<bool> IsUserOwnerOrParticipant(Travel travel, Guid userId)
    {
        if (userId != travel.OwnerId || (!travel.ParticipantIds?.Contains(userId) ?? false))
        {
            return false;
        }
        return true;
    }
}