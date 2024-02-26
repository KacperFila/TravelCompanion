using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class TravelPointRepository : ITravelPointRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<TravelPoint> _travelPoints;
    public TravelPointRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _travelPoints = _dbContext.TravelPoints;
    }

    public async Task AddAsync(TravelPoint travelPoint)
    {
        await _dbContext.AddAsync(travelPoint);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TravelPoint travelPoint)
    {
        _dbContext.Update(travelPoint);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<TravelPoint> GetAsync(Guid id)
    {
        return await _dbContext.TravelPoints.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}