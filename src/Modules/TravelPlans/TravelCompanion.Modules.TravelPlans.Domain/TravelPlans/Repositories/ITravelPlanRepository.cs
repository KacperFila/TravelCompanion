using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

public interface ITravelPlanRepository
{
    Task<TravelPlan> GetAsync(Guid id);
    Task AddAsync(TravelPlan travelPlan);
    Task<bool> ExistAsync(Guid id);
    Task UpdateAsync(Guid Id, TravelPlan travelPlan);
    Task DeleteAsync(Guid id);
}