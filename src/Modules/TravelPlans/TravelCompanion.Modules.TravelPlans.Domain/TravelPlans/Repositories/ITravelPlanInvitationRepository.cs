using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

public interface ITravelPlanInvitationRepository
{
    Task AddInvitationAsync(TravelPlanInvitation travelPlanInvitation);
    Task<TravelPlanInvitation> GetAsync(Guid invitationId);
    Task<bool> ExistsAsync(Guid invitationId);
    Task UpdateInvitationAsync(TravelPlanInvitation travelPlanInvitation);
    Task RemoveInvitationAsync(Guid invitationId);
}