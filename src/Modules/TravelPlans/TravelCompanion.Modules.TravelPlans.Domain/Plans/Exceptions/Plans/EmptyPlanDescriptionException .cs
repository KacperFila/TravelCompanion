using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class EmptyPlanDescriptionException : TravelCompanionException
{
    public Guid TravelPlanId { get; }
    public EmptyPlanDescriptionException(Guid travelPlanId) : base($"Travel plan with Id: {travelPlanId} defines an empty description.")
    {
        TravelPlanId = travelPlanId;
    }
}