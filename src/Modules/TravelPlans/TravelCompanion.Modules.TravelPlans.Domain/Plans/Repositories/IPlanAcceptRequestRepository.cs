using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface IPlanAcceptRequestRepository
{
    Task AddAsync(PlanAcceptRequest planAcceptRequest);
    Task<PlanAcceptRequest> GetByPlanAsync(Guid requestId);
    Task<bool> ExistsByPlanAsync(Guid planId);
    Task UpdateAsync(PlanAcceptRequest planAcceptRequest);
}