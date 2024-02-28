using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointSuggestions.Commands;

public record AcceptTravelPointSuggestion(Guid suggestionId) : ICommand;
