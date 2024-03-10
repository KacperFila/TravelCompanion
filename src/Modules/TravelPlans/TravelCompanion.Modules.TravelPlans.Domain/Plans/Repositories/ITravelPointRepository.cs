using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface ITravelPointRepository
{
    Task AddAsync(TravelPoint travelPoint);
    Task UpdateAsync(TravelPoint travelPoint);
    Task<List<TravelPoint>> GetAllForPlanAsync(Guid planId);
    Task<bool> ExistAsync(Guid id);
    Task<TravelPoint> GetAsync(Guid id);
    Task RemoveAsync(TravelPoint travelPoint);
}