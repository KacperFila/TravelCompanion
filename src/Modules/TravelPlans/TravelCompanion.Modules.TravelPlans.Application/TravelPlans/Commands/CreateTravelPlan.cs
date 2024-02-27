using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlans.Commands;

public record CreateTravelPlan(Guid ownerId, string title, string? description,
    DateOnly from, DateOnly to) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}