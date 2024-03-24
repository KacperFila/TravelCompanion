using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<Plan> _travelPlans;

    public PlanRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _travelPlans = _dbContext.Plans;
    }

    public async Task<Plan> GetAsync(Guid id)
    {
        return await _travelPlans
            .Include(x => x.AdditionalCosts)
            .Include(x => x.TravelPlanPoints)
            .ThenInclude(x => x.Receipts)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Plan> GetByPointIdAsync(Guid pointId)
    {
        return await _travelPlans
            .Include(x => x.AdditionalCosts)
            .Include(x => x.TravelPlanPoints)
            .ThenInclude(x => x.Receipts)
            .SingleOrDefaultAsync(x => x.TravelPlanPoints
                .Any(s => s.Id == pointId));
    }

    public async Task<List<Plan>> BrowseAsync()
    {
        return _travelPlans
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task AddAsync(Plan plan)
    {
        await _travelPlans.AddAsync(plan);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _travelPlans.AnyAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Plan plan)
    {
        _travelPlans.Update(plan);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var plan = await GetAsync(id);
        _dbContext.Remove(plan);
        await _dbContext.SaveChangesAsync();
    }
}