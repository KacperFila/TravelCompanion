using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class ParticipantAlreadyAcceptedException : TravelCompanionException
{
    public Guid Id { get; }
    public ParticipantAlreadyAcceptedException(Guid id) : base($"Participant with Id: {id} already accepted given plan.")
    {
        Id = id;
    }
}