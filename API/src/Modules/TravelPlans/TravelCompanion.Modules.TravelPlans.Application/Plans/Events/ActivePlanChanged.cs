using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Events;

public record ActivePlanChanged(Guid userId, Guid planId) : IEvent;