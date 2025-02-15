using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;

public record PointReceiptAdded(Guid planId, decimal amount) : IEvent;
