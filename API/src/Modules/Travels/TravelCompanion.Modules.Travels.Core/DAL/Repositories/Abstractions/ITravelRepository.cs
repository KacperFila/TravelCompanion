using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;

public interface ITravelRepository
{
    Task<Travel?> GetAsync(Guid id);
    Task<int> GetTravelsCountAsync(Guid userId);
    Task<int> GetFinishedTravelsCountAsync(Guid userId);
    Task<Dictionary<Guid, int>> GetCommonTravelsCountsAsync(Guid userId, IEnumerable<Guid> companionIds);
    Task<int> GetCommonTravelsCount(Guid userId, Guid companionId);
    Task<List<Guid>> GetTopFrequentCompanionsAsync(Guid userId, int count);
    Task<List<Travel>> GetUpcomingTravelsAsync(Guid userId);
    Task<bool> ExistAsync(Guid id);
    Task<List<Travel>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder);
    Task<List<Travel?>> GetForUserAsync(Guid userId);
    Task AddAsync(Travel? travel);
    Task UpdateAsync(Travel? travel);
    Task DeleteAsync(Guid id);
    Task AddTravelRatingAsync(TravelRating travelRating);
    Task UpdateTravelRatingAsync(TravelRating travelRating);
    Task RemoveTravelRatingAsync(TravelRating travelRating);
}