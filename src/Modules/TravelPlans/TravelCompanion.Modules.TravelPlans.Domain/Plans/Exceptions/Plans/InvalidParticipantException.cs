using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class InvalidParticipantException : TravelCompanionException
{
    public Guid Id { get; }
    public InvalidParticipantException(Guid id) : base($"Participant with Id: {id} is not valid.")
    {
        Id = id;
    }
}