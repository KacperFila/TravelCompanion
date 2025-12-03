using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface ITravelPointRemoveRequestRepository
{
    Task AddAsync(TravelPointRemoveRequest request);
    Task<TravelPointRemoveRequest> GetAsync(Guid requestId);
    Task RemoveAsync(TravelPointRemoveRequest request);
}