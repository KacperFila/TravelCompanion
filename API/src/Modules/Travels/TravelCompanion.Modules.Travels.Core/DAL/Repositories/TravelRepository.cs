﻿using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories;

internal class TravelRepository : ITravelRepository
{
    private readonly TravelsDbContext _dbContext;
    private readonly DbSet<Travel?> _travels;
    public TravelRepository(TravelsDbContext dbContext)
    {
        _dbContext = dbContext;
        _travels = _dbContext.Travels;
    }

    public async Task<Travel?> GetAsync(Guid id)
    {
        return await _travels
            .Include(x => x.Ratings)
            .Include(x => x.TravelPoints)
            .Include(x => x.AdditionalCosts)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> GetTravelsCountAsync(Guid userId)
    {
        return await _travels.CountAsync(x => x.OwnerId == userId 
                                        || x.ParticipantIds!.Contains(userId));
    }
    
    public async Task<int> GetFinishedTravelsCountAsync(Guid userId)
    {
        return await _travels.CountAsync(x => x.OwnerId == userId 
                                              || x.ParticipantIds!.Contains(userId)
                                              && x.IsFinished);
    }

    public async Task<Dictionary<Guid, int>> GetCommonTravelsCountsAsync(Guid userId, IEnumerable<Guid> companionIds)
    {
        var commonTravels = await _travels
            .Where(x => x.ParticipantIds!.Contains(userId))
            .ToListAsync();

        var usersCompanions = commonTravels
            .SelectMany(x => x.ParticipantIds!)
            .Where(p => p != userId && companionIds.Contains(p))
            .GroupBy(pid => pid)
            .Select(g => new { CompanionId = g.Key, Count = g.Count() });

        return usersCompanions.ToDictionary(x => x.CompanionId, x => x.Count);
    }

    public async Task<int> GetCommonTravelsCount(Guid userId, Guid companionId)
    {
        return await _travels
            .Where(x => x.ParticipantIds.Contains(userId)
                        && x.ParticipantIds.Contains(companionId))
            .CountAsync();
    }

    public async Task<List<Guid>> GetTopFrequentCompanionsAsync(Guid userId, int count)
    {
        var travels = await _dbContext.Travels
            .Where(x => x.ParticipantIds.Contains(userId))
            .ToListAsync();

        var companionCounts = travels
            .SelectMany(t => t.ParticipantIds.Where(pid => pid != userId))
            .GroupBy(pid => pid)
            .OrderByDescending(g => g.Count())
            .Take(count)
            .Select(g => g.Key)
            .ToList();

        return companionCounts;
    }


    public async Task<List<Travel>> GetUpcomingTravelsAsync(Guid userId)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var futureTravels = _travels
            .Where(x => x.ParticipantIds!.Contains(userId) && x.From >= today);

        var nextDate = await futureTravels
            .OrderBy(x => x.From)
            .Select(x => x.From)
            .FirstOrDefaultAsync();

        if (nextDate == default)
            return new List<Travel>();

        return await futureTravels
            .Where(x => x.From == nextDate)
            .ToListAsync();
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

    public async Task<List<Travel?>> GetForUserAsync(Guid userId)
    {
        return await _travels
            .Where(x => x.ParticipantIds!
            .Contains(userId))
            .ToListAsync();
    }

    public async Task AddAsync(Travel? travel)
    {
        await _travels.AddAsync(travel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Travel? travel)
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