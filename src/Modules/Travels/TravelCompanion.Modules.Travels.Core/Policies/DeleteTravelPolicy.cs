using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Policies;

public class DeleteTravelPolicy : IDeleteTravelPolicy
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