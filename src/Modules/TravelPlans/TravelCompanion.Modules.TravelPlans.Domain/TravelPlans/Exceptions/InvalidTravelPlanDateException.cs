using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class InvalidTravelPlanDateException : TravelCompanionException
{
    public Guid TravelPlanId { get; }
    public InvalidTravelPlanDateException(Guid travelPlanId) : base($"Travel Plan with Id: {travelPlanId} defines invalid dates.")
    {
        TravelPlanId = travelPlanId;
    }
}