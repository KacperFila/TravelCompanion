using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

public interface ITravelPointRepository
{
    Task AddAsync(TravelPoint travelPoint);
    Task UpdateAsync(TravelPoint travelPoint);
    Task<bool> ExistAsync(Guid id);
    Task<TravelPoint> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
}