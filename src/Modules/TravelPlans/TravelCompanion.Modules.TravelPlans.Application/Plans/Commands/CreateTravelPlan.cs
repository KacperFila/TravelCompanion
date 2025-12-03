using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;

public record CreateTravelPlan(string Title, string? Description,
    DateOnly? From, DateOnly? To) : ICommand
{ }