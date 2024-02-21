﻿using Microsoft.EntityFrameworkCore;
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
        var travel = await _travels.SingleOrDefaultAsync(x => x.Id == id);   
        return travel;
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _travels.AnyAsync(x => x.Id == id);
    }

    public async Task<List<Travel>> GetAllAsync()
    {
        var results = await _travels.ToListAsync();
        return results;
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
}