using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;

public interface ITravelPointSuggestionsRepository
{
    Task AddAsync(TravelPointChangeSuggestion suggestion);
    Task<TravelPointChangeSuggestion> GetAsync(Guid suggestionId);
    Task RemoveAsync(TravelPointChangeSuggestion suggestion);
}