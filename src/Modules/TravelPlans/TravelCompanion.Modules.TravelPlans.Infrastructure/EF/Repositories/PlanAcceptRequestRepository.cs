using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class PlanAcceptRequestRepository : IPlanAcceptRequestRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<PlanAcceptRequest> _planAcceptRequests;
    public PlanAcceptRequestRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _planAcceptRequests = _dbContext.PlanAcceptRequests;
    }

    public async Task AddAsync(PlanAcceptRequest planAcceptRequest)
    {
        await _planAcceptRequests.AddAsync(planAcceptRequest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PlanAcceptRequest> GetByPlanAsync(Guid planId)
    {
        return await _planAcceptRequests.SingleOrDefaultAsync(x => x.PlanId == planId);
    }

    public async Task<bool> ExistsByPlanAsync(Guid planId)
    {
        return await _planAcceptRequests.AnyAsync(x => x.PlanId == planId);
    }

    public async Task UpdateAsync(PlanAcceptRequest planAcceptRequest)
    {
        _planAcceptRequests.Update(planAcceptRequest);
        await _dbContext.SaveChangesAsync();
    }
}