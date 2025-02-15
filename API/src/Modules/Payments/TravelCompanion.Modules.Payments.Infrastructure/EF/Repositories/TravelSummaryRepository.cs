using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Payments.Domain.Payments.Entities;
using TravelCompanion.Modules.Payments.Domain.Payments.Repositories;

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Repositories;

public class TravelSummaryRepository : ITravelSummaryRepository
{
    private PaymentsDbContext _dbContext;
    private DbSet<TravelSummary> _summaries;

    public TravelSummaryRepository(PaymentsDbContext dbContext)
    {
        _dbContext = dbContext;
        _summaries = _dbContext.TravelSummaries;
    }

    public async Task AddTravelSummary(TravelSummary travelSummary)
    {
        _summaries.Add(travelSummary);
        await _dbContext.SaveChangesAsync();
    }
}