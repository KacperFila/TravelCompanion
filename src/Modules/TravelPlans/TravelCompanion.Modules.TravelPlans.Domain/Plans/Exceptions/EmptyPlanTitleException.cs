using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class EmptyPlanTitleException : TravelCompanionException
{
    public Guid TravelPlanId { get; }
    public EmptyPlanTitleException(Guid travelPlanId) : base($"Travel plan with Id: {travelPlanId} defines an empty title.")
    {
        TravelPlanId = travelPlanId;
    }
}