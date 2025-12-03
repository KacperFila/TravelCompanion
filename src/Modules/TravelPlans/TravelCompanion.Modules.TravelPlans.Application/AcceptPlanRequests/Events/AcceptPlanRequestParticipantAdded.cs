using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Events;

public record AcceptPlanRequestParticipantAdded(Guid ParticipantId, Guid PlanId) : IEvent;