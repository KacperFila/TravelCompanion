using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;

public interface ITravelPointRepository
{
    Task<List<TravelPoint>> GetForTravelAsync(Guid travelId);
    Task<TravelPoint> GetAsync(Guid pointId);
    Task UpdateAsync(TravelPoint travelPoint);
}