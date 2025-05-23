using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;

public interface ITravelRepository
{
    Task<Travel?> GetAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<List<Travel>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder);
    Task AddAsync(Travel travel);
    Task UpdateAsync(Travel travel);
    Task DeleteAsync(Guid id);
    Task AddTravelRatingAsync(TravelRating travelRating);
    Task UpdateTravelRatingAsync(TravelRating travelRating);
    Task RemoveTravelRatingAsync(TravelRating travelRating);
}