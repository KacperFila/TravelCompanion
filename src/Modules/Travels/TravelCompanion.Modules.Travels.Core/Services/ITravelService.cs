using TravelCompanion.Modules.Travels.Core.Dto;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal interface ITravelService
{
    Task AddAsync(TravelDto travel);
    Task<TravelDto> GetAsync(Guid TravelId);
    Task<IReadOnlyList<TravelDto>> GetAllAsync();
    Task RateAsync(Guid TravelId, int Rating);
    Task RemoveRatingAsync(Guid TravelId);
    Task DeleteAsync(Guid TravelId);
}