using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Repositories;

public interface ITravelRepository
{
    Task<Travel> GetAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<IReadOnlyList<Travel>> GetAllAsync();
    Task AddAsync(Travel travel);
    Task UpdateAsync(Travel travel);
    Task DeleteAsync(Guid id);
}