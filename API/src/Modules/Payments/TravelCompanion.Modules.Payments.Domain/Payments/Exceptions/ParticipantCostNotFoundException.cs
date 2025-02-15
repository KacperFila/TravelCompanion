using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;

public class ParticipantCostNotFoundException : TravelCompanionException
{
    public Guid ParticipantId { get; set; }
    public ParticipantCostNotFoundException(Guid participantId) : base($"Cost for participant with Id: {participantId} was not found.")
    {
        ParticipantId = participantId;
    }
}