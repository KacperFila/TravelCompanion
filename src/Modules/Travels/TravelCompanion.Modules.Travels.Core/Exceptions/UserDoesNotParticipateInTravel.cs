using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class UserDoesNotParticipateInTravel : TravelCompanionException
{
public Guid Id { get; set; }
public UserDoesNotParticipateInTravel(Guid travelId) : base($"Current user does not participate in travel with Id: {travelId}.")
{
    Id = travelId;
}
}