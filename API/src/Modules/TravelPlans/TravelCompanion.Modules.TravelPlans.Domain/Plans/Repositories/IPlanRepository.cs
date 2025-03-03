using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface IPlanRepository
{
    Task<Plan> GetAsync(Guid id);
    Task<Plan> GetByPointIdAsync(Guid pointId);
    Task<Paged<Plan>> BrowseForUserAsync(Guid userId, int page, int results, string sortOrder, string sortBy);
    Task AddAsync(Plan plan);
    Task<bool> ExistAsync(Guid id);
    Task UpdateAsync(Plan plan);
    Task DeleteAsync(Guid id);
}