using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface ITravelPointUpdateRequestRepository
{
    Task AddAsync(TravelPointUpdateRequest request);
    Task<TravelPointUpdateRequest> GetAsync(Guid requestId);
    Task RemoveAsync(TravelPointUpdateRequest request);
    Task<List<TravelPointUpdateRequest>> GetUpdateRequestsForPointAsync(Guid pointId);
}