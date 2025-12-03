using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Travels.Core.Events.External;

public record PlanAccepted(
    Guid PlanId,
    List<Guid> Participants,
    Guid OwnerId,
    string Title,
    string Description,
    DateOnly? From,
    DateOnly? To,
    List<Guid> AdditionalCostIds,
    decimal AdditionalCostsValue,
    List<Guid> PlanPointIds,
    decimal TotalCost) : IEvent;