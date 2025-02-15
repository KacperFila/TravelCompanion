using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class InvalidPlanDateException : TravelCompanionException
{
    public Guid TravelPlanId { get; }
    public InvalidPlanDateException(Guid travelPlanId) : base($"Travel plan with Id: {travelPlanId} defines invalid dates.")
    {
        TravelPlanId = travelPlanId;
    }
}