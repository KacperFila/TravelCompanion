using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class ReceiptRepository : IReceiptRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<Receipt> _receipts;
    public ReceiptRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _receipts = _dbContext.Receipts;
    }

    public async Task AddAsync(Receipt receipt)
    {
        await _receipts.AddAsync(receipt);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Receipt> GetAsync(Guid receiptId)
    {
        return await _receipts.SingleOrDefaultAsync(x => x.Id == receiptId);
    }

    public async Task<List<Receipt>> BrowseForPlanAsync(Guid planId)
    {
        return await _receipts
            .AsNoTracking()
            .Where(x => x.PlanId == planId)
            .ToListAsync();
    }

    public async Task<List<Receipt>> BrowseForPointAsync(Guid pointId)
    {
        return await _receipts
        .AsNoTracking()
            .Where(x => x.PointId == pointId)
            .ToListAsync();
    }

    public async Task RemoveAsync(Receipt receipt)
    {
        _receipts.Remove(receipt);
        await _dbContext.SaveChangesAsync();
    }
}