using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class ParticipantAlreadyAddedException : TravelCompanionException
{
    public Guid ParticipantId { get; set; }
    public ParticipantAlreadyAddedException(Guid participantId) : base($"Participant with Id: {participantId} is already added.")
    {
        ParticipantId = participantId;
    }
}