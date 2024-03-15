using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

namespace TravelCompanion.Modules.Travels.Core.Policies;

public class TravelPolicy : ITravelPolicy
{
    public bool CanDelete(Travel travel)
    {
        if (!travel.AllParticipantsPaid)
        {
            return false;
        }

        return true;
    }

    public bool IsUserOwnerOrParticipant(Travel travel, Guid userId)
    {
        if (userId != travel.OwnerId || (!travel.ParticipantIds?.Contains(userId) ?? false))
        {
            return false;
        }
        return true;
    }

    public bool DoesUserParticipate(Travel travel, Guid userId)
    {
        if (!travel.ParticipantIds.Contains(userId))
        {
            return false;
        }
        return true;
    }
}