using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class EmptyTravelPlanDescriptionException : TravelCompanionException
{
    public Guid TravelPlanId { get; }
    public EmptyTravelPlanDescriptionException(Guid travelPlanId) : base($"Travel Plan with Id: {travelPlanId} defines an empty description.")
    {
        TravelPlanId = travelPlanId;
    }
}