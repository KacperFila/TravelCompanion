using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Travels.Core.Events;

public record TravelFromPlanCreated(Guid PlanId) : IEvent;