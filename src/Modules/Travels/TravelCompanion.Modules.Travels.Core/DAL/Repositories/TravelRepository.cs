using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories;

internal class TravelRepository : ITravelRepository
{
    private readonly TravelsDbContext _dbContext;
    private readonly DbSet<Travel> _travels;
    public TravelRepository(TravelsDbContext dbContext)
    {
        _dbContext = dbContext;
        _travels = _dbContext.Travels;
    }

    public async Task<Travel> GetAsync(Guid id)
    {
        return await _travels
            .Include(x => x.Ratings)
            .Include(x => x.TravelPoints)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _travels.AnyAsync(x => x.Id == id);
    }

    public async Task<List<Travel>> GetAllAsync(string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var baseQuery = _travels
            .Include(x => x.AdditionalCosts)
            .Include(x => x.Ratings)
            .Include(x => x.TravelPoints)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            baseQuery = baseQuery.Where(
                x => x.Title.ToLower()
                    .Contains(searchTerm.ToLower()));
        }

        return await baseQuery.ToListAsync();
    }
    
    public async Task AddAsync(Travel travel)
    {
        await _travels.AddAsync(travel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Travel travel)
    {
        _travels.Update(travel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var travel = await GetAsync(id);
        _travels.Remove(travel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddTravelRatingAsync(TravelRating travelRating)
    {
        await _dbContext.AddAsync(travelRating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTravelRatingAsync(TravelRating travelRating)
    {
        _dbContext.Update(travelRating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveTravelRatingAsync(TravelRating travelRating)
    {
        _dbContext.Remove(travelRating);
        await _dbContext.SaveChangesAsync();
    }
}