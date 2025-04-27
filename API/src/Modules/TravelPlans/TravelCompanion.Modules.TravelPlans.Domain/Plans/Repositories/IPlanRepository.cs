using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface IPlanRepository
{
    Task<Plan> GetAsync(Guid id);
    Task<Plan> GetByPointIdAsync(Guid pointId);
    Task<List<Plan>> BrowseById(List<Guid> pointIds);
    Task<List<Plan>> BrowseForUserAsync(Guid userId);
    Task AddAsync(Plan plan);
    Task<bool> ExistAsync(Guid id);
    Task UpdateAsync(Plan plan);
    Task DeleteAsync(Guid id);
}