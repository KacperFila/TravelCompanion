using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;

public interface ITravelRepository
{
    Task<Travel> GetAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<List<Travel>> GetAllAsync();
    Task AddAsync(Travel travel);
    Task UpdateAsync(Travel travel);
    Task DeleteAsync(Guid id);
    Task AddTravelRating(TravelRating travelRating);
    Task UpdateTravelRating(TravelRating travelRating);

    Task RemoveTravelRating(TravelRating travelRating);
}