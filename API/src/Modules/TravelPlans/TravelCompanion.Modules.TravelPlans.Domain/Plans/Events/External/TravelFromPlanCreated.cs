using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Events.External;

public record TravelFromPlanCreated(Guid PlanId) : IEvent;