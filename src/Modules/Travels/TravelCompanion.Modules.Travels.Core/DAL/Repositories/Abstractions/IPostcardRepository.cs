using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Shared.DTO;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;

internal interface IPostcardRepository
{
    Task<Postcard> GetAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<IReadOnlyList<Postcard>> GetAllByTravelIdAsync(Guid travelId);
    Task AddAsync(Postcard postcard);
    Task UpdateAsync(Postcard postcard);
    Task DeleteAsync(Guid id);
    Task DeleteExpiredUnapprovedAsync();
    Task<List<Postcard>> GetLastYearPostcardsFromMonth(Guid userId, int month);
}