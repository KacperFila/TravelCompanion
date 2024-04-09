using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Modules.Travels.Core.Entities.Enums;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories;

internal sealed class PostcardRepository : IPostcardRepository
{
    private readonly TravelsDbContext _dbContext;
    private readonly DbSet<Postcard> _postcards;
    public PostcardRepository(TravelsDbContext dbContext)
    {
        _dbContext = dbContext;
        _postcards = _dbContext.Postcards;
    }

    public async Task<Postcard> GetAsync(Guid id)
    {
        return await _postcards.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _postcards.AnyAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Postcard>> GetAllByTravelIdAsync(Guid travelId)
    {
        return await _postcards.Where(x => x.TravelId == travelId).ToListAsync();
    }

    public async Task AddAsync(Postcard postcard)
    {
        await _postcards.AddAsync(postcard);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Postcard postcard)
    {
        _postcards.Update(postcard);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var postcard = await _postcards.SingleOrDefaultAsync(x => x.Id == id);
        _postcards.Remove(postcard);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteExpiredUnapprovedAsync()
    {
        var postcards = await _postcards.Where(
            x => x.CreatedOnUtc < DateTime.UtcNow.AddDays(-7) &&
                 x.Status == PostcardStatus.Rejected || x.Status == PostcardStatus.Pending)
            .ToListAsync();

        _postcards.RemoveRange(postcards);
        await _dbContext.SaveChangesAsync();
    }
}