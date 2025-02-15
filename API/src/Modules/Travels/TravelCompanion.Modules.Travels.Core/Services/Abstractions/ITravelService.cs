using TravelCompanion.Modules.Travels.Core.DTO;

namespace TravelCompanion.Modules.Travels.Core.Services.Abstractions;

internal interface ITravelService
{
    Task<TravelDetailsDTO> GetAsync(Guid TravelId);
    Task<IReadOnlyList<TravelDetailsDTO>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder);
    Task RateAsync(Guid TravelId, int Rating);
    Task VisitTravelPointAsync(Guid pointId);
    Task RemoveRatingAsync(Guid TravelId);
    Task DeleteAsync(Guid TravelId);
}