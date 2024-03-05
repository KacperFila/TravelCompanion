using Microsoft.EntityFrameworkCore;
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
}