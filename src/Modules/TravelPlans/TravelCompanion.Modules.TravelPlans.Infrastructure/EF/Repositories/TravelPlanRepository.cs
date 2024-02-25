using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class TravelPlanRepository : ITravelPlanRepository
{
    public async Task<TravelPlan> GetAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(TravelPlan travelPlan)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(TravelPlan travelPlan)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }
}