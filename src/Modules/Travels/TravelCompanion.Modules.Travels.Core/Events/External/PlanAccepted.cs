using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Travels.Core.Events.External;

public record PlanAccepted(IList<Guid> participants, Guid ownerId, string title, string description, DateOnly from,
    DateOnly to, IList<Receipt> additionalCosts, decimal additionalCostsValue, IList<TravelPoint> planPoints) : IEvent;