using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Policies.Abstractions;

namespace TravelCompanion.Modules.Travels.Core.Policies;

public class TravelPolicy : ITravelPolicy
{
    public async Task<bool> CanDeleteAsync(Travel travel)
    {
        if (!travel.allParticipantsPaid)
        {
            return false;
        }

        return true;
    }
}