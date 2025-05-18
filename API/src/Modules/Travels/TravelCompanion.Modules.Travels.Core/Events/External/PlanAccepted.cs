using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Travels.Core.Events.External;

public record PlanAccepted(
    Guid PlanId,
    IList<Guid> Participants,
    Guid OwnerId,
    string Title,
    string Description,
    DateOnly? From,
    DateOnly? To,
    IList<Guid> AdditionalCostIds,
    decimal AdditionalCostsValue,
    IList<Guid> PlanPointIds,
    decimal TotalCost) : IEvent;