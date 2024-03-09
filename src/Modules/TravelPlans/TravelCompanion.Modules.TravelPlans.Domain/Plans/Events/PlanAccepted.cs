using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;

public record PlanAccepted(
    IList<Guid> participants,
    Guid ownerId,
    string title,
    string description,
    DateOnly from,
    DateOnly to,
    IList<Receipt> additionalCosts,
    decimal additionalCostsValue,
    IList<TravelPoint> planPoints) : IEvent;