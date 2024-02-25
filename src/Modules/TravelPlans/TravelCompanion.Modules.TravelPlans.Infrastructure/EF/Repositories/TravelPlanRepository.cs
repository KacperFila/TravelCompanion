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
        throw new NotImplementedException();
    }

    public async Task AddAsync(TravelPlan travelPlan)
    {
        await _travelPlans.AddAsync(travelPlan);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, TravelPlan travelPlan)
    {
       throw new NotSupportedException();
        
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}