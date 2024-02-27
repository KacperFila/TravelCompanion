using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Repositories;

public class TravelPointSuggestionRepository : ITravelPointSuggestionsRepository
{
    private readonly TravelPlansDbContext _dbContext;
    private readonly DbSet<TravelPointChangeSuggestion> _suggestions;

    public TravelPointSuggestionRepository(TravelPlansDbContext dbContext)
    {
        _dbContext = dbContext;
        _suggestions = _dbContext.TravelPointChangeSuggestions;
    }
    public async Task AddAsync(TravelPointChangeSuggestion suggestion)
    {
        await _suggestions.AddAsync(suggestion);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TravelPointChangeSuggestion> GetAsync(Guid suggestionId)
    {
        return await _suggestions.SingleOrDefaultAsync(x => x.SuggestionId == suggestionId);
    }

    public async Task RemoveAsync(TravelPointChangeSuggestion suggestion)
    {
        _suggestions.Remove(suggestion);
        _dbContext.SaveChangesAsync();
    }
}