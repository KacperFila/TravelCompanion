using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

public interface ITravelPlanRepository
{
    Task<TravelPlan> GetAsync(AggregateId id);
    Task AddAsync(TravelPlan travelPlan);
    Task UpdateAsync(TravelPlan travelPlan);
    Task DeleteAsync(AggregateId id);
}