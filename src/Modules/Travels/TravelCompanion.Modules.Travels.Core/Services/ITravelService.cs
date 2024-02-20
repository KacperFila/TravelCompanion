using TravelCompanion.Modules.Travels.Core.Dto;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal interface ITravelService
{
    Task AddAsync(TravelDto travel);
    Task<TravelDto> GetAsync(Guid id);
    Task<IReadOnlyList<TravelDto>> GetAllAsync();
    Task UpdateAsync(Guid TravelId, TravelDto dto);
    Task DeleteAsync(Guid id);
}