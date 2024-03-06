using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

public class CouldNotModifyNotAcceptedTravelPointException : TravelCompanionException
{
    public CouldNotModifyNotAcceptedTravelPointException() : base("Could not modify not accepted exception")
    {
    }
}