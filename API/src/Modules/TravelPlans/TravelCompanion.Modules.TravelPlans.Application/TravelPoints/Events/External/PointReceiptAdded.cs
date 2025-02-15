using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Events.External;

public record PointReceiptAdded(Guid planId, decimal amount) : IEvent;