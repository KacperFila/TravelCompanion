using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class TravelPointUpdateRequestRepository : ITravelPointUpdateRequestRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<TravelPointUpdateRequest> _requests;

    public TravelPointUpdateRequestRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _requests = _dbContext.TravelPointUpdateRequests;
    }
    public async Task AddAsync(TravelPointUpdateRequest request)
    {
        await _requests.AddAsync(request);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TravelPointUpdateRequest> GetAsync(Guid requestId)
    {
        return await _requests.SingleOrDefaultAsync(x => x.RequestId == requestId);
    }

    public async Task RemoveAsync(TravelPointUpdateRequest request)
    {
        _requests.Remove(request);
        await _dbContext.SaveChangesAsync();
    }
}