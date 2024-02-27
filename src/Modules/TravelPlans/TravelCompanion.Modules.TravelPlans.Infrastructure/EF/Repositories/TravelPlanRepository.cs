using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class TravelPlanRepository : ITravelPlanRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<TravelPlan> _travelPlans;

    public TravelPlanRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _travelPlans = _dbContext.TravelPlans;
    }

    public async Task<TravelPlan> GetAsync(Guid id)
    {
        return await _travelPlans.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(TravelPlan travelPlan)
    {
        await _travelPlans.AddAsync(travelPlan);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _travelPlans.AnyAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(TravelPlan travelPlan)
    {
        _travelPlans.Update(travelPlan);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}