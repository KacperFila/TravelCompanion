using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Events;

public record ParticipantAddedToPlan(Guid participantId, Guid planId) : IEvent;
