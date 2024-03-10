using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;

public record PlanAccepted(
    Guid planId,
    IList<Guid> participants,
    Guid ownerId,
    string title,
    string description,
    DateOnly from,
    DateOnly to,
    IList<Guid> additionalCostIds,
    decimal additionalCostsValue,
    IList<Guid> planPointIds) : IEvent;