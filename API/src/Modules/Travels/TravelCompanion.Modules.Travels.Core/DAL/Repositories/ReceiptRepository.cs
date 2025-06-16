using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories;

internal class ReceiptRepository : IReceiptRepository
{
    private readonly TravelsDbContext _context;
    private readonly DbSet<Receipt> _receipts;
    
    public ReceiptRepository(TravelsDbContext context)
    {
        _context = context;
        _receipts = _context.Receipts;
    }

    public async Task AddAsync(Receipt receipt)
    {
        await _receipts.AddAsync(receipt);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Receipt>> BrowseForPointAsync(Guid pointId)
    {
        return await _receipts
            .Where(x => x.TravelPointId == pointId)
            .ToListAsync();
    }
}