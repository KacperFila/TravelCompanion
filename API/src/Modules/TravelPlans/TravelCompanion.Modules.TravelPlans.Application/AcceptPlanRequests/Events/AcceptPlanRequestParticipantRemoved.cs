using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Events;

public record AcceptPlanRequestParticipantRemoved(Guid participantId, Guid planId) : IEvent;