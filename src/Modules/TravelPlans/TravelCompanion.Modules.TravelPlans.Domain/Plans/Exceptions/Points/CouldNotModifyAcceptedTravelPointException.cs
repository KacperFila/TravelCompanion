using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

public class CouldNotModifyAcceptedTravelPointException : TravelCompanionException
{
    public CouldNotModifyAcceptedTravelPointException() : base("Could not modify accepted travel point")
    {
    }
}