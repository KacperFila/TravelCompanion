using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Travels.Core.Events.External;

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
    IList<Guid> planPointIds,
    decimal totalCost) : IEvent;