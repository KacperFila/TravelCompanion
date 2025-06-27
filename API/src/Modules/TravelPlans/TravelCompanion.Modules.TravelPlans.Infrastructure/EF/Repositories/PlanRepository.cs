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
        var plan = await _travelPlans
            .Include(x => x.Participants)
            .Include(x => x.AdditionalCosts)
            .Include(x => x.TravelPlanPoints)
                .ThenInclude(x => x.Receipts)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (plan != null)
        {
            plan.ReorderTravelPoints();
        }

        return plan;
    }

    public async Task<int> GetPlanCountAsync(Guid userId)
    {
        return await _dbContext.Plans.CountAsync(
            x => x.OwnerId == userId
            || x.Participants.Any(y => y.ParticipantId == userId));
    }

    public async Task<Plan> GetByPointIdAsync(Guid pointId)
    {
        return await _travelPlans
            .Include(x => x.Participants)
            .Include(x => x.AdditionalCosts)
            .Include(x => x.TravelPlanPoints)
            .ThenInclude(x => x.Receipts)
            .SingleOrDefaultAsync(x => x.TravelPlanPoints
                .Any(s => s.Id == pointId));
    }

    public async Task<List<Plan>> BrowseForUserAsync(Guid userId)
    {
        var query = _travelPlans
            .AsNoTracking()
            .Where(x => x.Participants
                .Any(p => p.ParticipantId == userId));

        return await query.ToListAsync();
    }

    public async Task<List<Plan?>> GetPlansForParticipantsExcludingPlan(IEnumerable<Guid> userIds, Guid excludedPlanId)
    {
        return await _travelPlans
            .AsNoTracking()
            .Include(p => p.Participants)
            .Where(p => p.Id != excludedPlanId && p.Participants.Any(pp => userIds.Contains(pp.ParticipantId)))
            .ToListAsync();
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

    public async Task<List<Plan>> BrowseById(List<Guid> pointIds)
    {
        var plans = await _travelPlans
            .Where(x => pointIds.Contains(x.Id)).ToListAsync();
        return plans;
    }
    

}