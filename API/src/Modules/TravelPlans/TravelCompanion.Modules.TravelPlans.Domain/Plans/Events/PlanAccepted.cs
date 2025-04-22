using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;

public record PlanAccepted(
    Guid planId,
    IEnumerable<Guid> participants,
    Guid ownerId,
    string title,
    string description,
    DateOnly? from,
    DateOnly? to,
    IEnumerable<Guid> additionalCostIds,
    decimal additionalCostsValue,
    IEnumerable<Guid> planPointIds,
    decimal totalCost) : IEvent;