using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class CouldNotModifyNotAcceptedTravelPointException : TravelCompanionException
{
    public CouldNotModifyNotAcceptedTravelPointException() : base("Could not modify not accepted exception")
    {
    }
}