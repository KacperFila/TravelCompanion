using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;

public record CreateTravelPlan(string title, string? description,
    DateOnly from, DateOnly to) : ICommand
{ }