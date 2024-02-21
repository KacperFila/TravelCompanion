using TravelCompanion.Modules.Travels.Core.Dto;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal interface ITravelService
{
    Task AddAsync(TravelDto travel);
    Task<TravelDto> GetAsync(Guid TravelId);
    Task<IReadOnlyList<TravelDto>> GetAllAsync();
    Task UpdateAsync(Guid TravelId, TravelDto dto);
    Task DeleteAsync(Guid TravelId);
}