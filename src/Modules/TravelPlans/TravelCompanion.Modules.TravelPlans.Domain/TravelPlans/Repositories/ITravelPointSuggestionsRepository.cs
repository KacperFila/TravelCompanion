using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;

public interface ITravelPointSuggestionsRepository
{
    Task AddAsync(TravelPointChangeSuggestion suggestion);
    Task<TravelPointChangeSuggestion> GetAsync(Guid suggestionId);
    Task RemoveAsync(TravelPointChangeSuggestion suggestion);
}