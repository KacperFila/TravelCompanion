using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class TravelPointRemoveRequestRepository : ITravelPointRemoveRequestRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<TravelPointRemoveRequest> _travelPointRemoveRequests;

    public TravelPointRemoveRequestRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _travelPointRemoveRequests = _dbContext.TravelPointRemoveRequests;
    }

    public async Task AddAsync(TravelPointRemoveRequest request)
    {
        await _travelPointRemoveRequests.AddAsync(request);
        await _dbContext.SaveChangesAsync();
    }
}