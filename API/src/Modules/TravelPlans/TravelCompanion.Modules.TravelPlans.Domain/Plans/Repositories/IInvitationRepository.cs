using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface IInvitationRepository
{
    Task AddAsync(Invitation invitation);
    Task<Invitation> GetAsync(Guid invitationId);
    Task<List<Invitation>> GetInvitationsForUser(Guid userId);
    Task<bool> ExistsByIdAsync(Guid invitationId);
    Task<bool> ExistsForUserAndTravelPlanAsync(Guid userId, Guid travelPlanId);
    Task UpdateAsync(Invitation invitation);
    Task RemoveAsync(Guid invitationId);
}