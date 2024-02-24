using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class EmptyTravelPlanTitleException : TravelCompanionException
{
    public Guid TravelPlanId { get; }
    public EmptyTravelPlanTitleException(Guid travelPlanId) : base($"Travel Plan with Id: {travelPlanId} defines an empty title.")
    {
        TravelPlanId = travelPlanId;
    }
}