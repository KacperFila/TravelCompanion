using TravelCompanion.Modules.Travels.Core.DTO;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Services.Abstractions;

internal interface ITravelService
{
    Task<TravelDetailsDto?> GetAsync(Guid travelId);
    Task<TravelDetailsDto?> GetActiveAsync();
    Task<List<TravelDetailsDto>> GetUserUpcomingTravelsAsync();
    Task<List<CommonTravelCompanionDTO>> GetTopFrequentCompanionsAsync();
    Task<IReadOnlyList<TravelDetailsDto?>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder);
    Task<IReadOnlyList<TravelDetailsDto?>> GetUserTravelsAsync();
    Task<int> GetUserTravelsCountAsync();
    Task<int> GetUserFinishedTravelsCountAsync();
    Task ChangeActiveTravelAsync(Guid travelId);
    Task RateAsync(Guid travelId, int rating);
    Task VisitTravelPointAsync(Guid pointId);
    Task UnvisitTravelPointAsync(Guid pointId);
    Task RemoveRatingAsync(Guid travelId);
    Task DeleteAsync(Guid travelId);
    Task AddReceipt(Guid travelPointId, List<Guid> participantsIds, Money amount, string? description);
    Task CompleteTravel(Guid travelId);
}