using TravelCompanion.Modules.Travels.Core.DTO;

namespace TravelCompanion.Modules.Travels.Core.Services.Abstractions;

internal interface ITravelService
{
    Task<TravelDetailsDto?> GetAsync(Guid travelId);
    Task<TravelDetailsDto?> GetActiveAsync();
    Task<IReadOnlyList<TravelDetailsDto?>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder);
    Task ChangeActiveTravelAsync(Guid travelId);
    Task RateAsync(Guid travelId, int rating);
    Task VisitTravelPointAsync(Guid pointId);
    Task RemoveRatingAsync(Guid travelId);
    Task DeleteAsync(Guid travelId);
}