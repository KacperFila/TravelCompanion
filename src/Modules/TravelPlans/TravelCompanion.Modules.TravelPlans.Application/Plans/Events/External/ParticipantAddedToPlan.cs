using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Events.External;

public record ParticipantAddedToPlan(Guid ParticipantId, Guid PlanId) : IEvent;
