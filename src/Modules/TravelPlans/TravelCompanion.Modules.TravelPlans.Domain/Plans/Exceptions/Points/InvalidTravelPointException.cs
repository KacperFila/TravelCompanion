using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

public class InvalidTravelPointException : TravelCompanionException
{
    public InvalidTravelPointException() : base("Given Travel Point is not valid.")
    {
    }
}