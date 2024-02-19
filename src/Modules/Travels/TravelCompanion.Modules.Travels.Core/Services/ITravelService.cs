using TravelCompanion.Modules.Travels.Core.Dto;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Services;

internal interface ITravelService
{
    Task AddAsync(TravelDto travel);
    Task<TravelDto> GetAsync(Guid id);
    Task<IReadOnlyList<TravelDto>> GetAllAsync();
    Task UpdateAsync(TravelDto dto);
    Task DeleteAsync(Guid id);
}