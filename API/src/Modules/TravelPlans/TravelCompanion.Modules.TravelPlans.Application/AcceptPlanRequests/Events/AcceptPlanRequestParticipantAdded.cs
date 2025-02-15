using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Events;

public record AcceptPlanRequestParticipantAdded(Guid participantId, Guid planId) : IEvent;