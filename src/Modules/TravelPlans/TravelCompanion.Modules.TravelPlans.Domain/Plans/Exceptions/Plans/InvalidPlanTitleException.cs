using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class InvalidPlanTitleException : TravelCompanionException
{
    public InvalidPlanTitleException() : base($"Title length should be at least 3 signs.")
    {
    }
}