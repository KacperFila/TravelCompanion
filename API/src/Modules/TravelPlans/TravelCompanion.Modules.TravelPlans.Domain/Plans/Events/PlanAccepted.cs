using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;

public record PlanAccepted(
    Guid PlanId,
    IEnumerable<Guid> Participants,
    Guid OwnerId,
    string Title,
    string Description,
    DateOnly? From,
    DateOnly? To,
    IEnumerable<Guid> AdditionalCostIds,
    decimal AdditionalCostsValue,
    IEnumerable<Guid> PlanPointIds,
    decimal TotalCost) : IEvent;