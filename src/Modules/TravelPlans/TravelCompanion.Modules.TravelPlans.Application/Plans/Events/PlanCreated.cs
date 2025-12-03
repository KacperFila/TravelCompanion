using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Events;

public record PlanCreated(Guid OwnerId, Guid PlanId) : IEvent;