using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class InvalidTravelPointException : TravelCompanionException
{
    public InvalidTravelPointException() : base("Given Travel Point is not valid.")
    {
    }
}