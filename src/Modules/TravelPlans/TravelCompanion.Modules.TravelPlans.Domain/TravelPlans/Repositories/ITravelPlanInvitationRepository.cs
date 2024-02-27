using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

public interface ITravelPlanInvitationRepository
{
    Task AddInvitationAsync(TravelPlanInvitation travelPlanInvitation);
    Task<TravelPlanInvitation> GetAsync(Guid invitationId);
    Task<bool> ExistsByIdAsync(Guid invitationId);
    Task<bool> ExistsForUserAndTravelPlanAsync(Guid userId, Guid travelPlanId);
    Task UpdateInvitationAsync(TravelPlanInvitation travelPlanInvitation);
    Task RemoveInvitationAsync(Guid invitationId);
}