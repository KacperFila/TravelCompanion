using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class UserNotAllowedToChangePlanException : TravelCompanionException
{
    public Guid PlanId { get; set; }
    public UserNotAllowedToChangePlanException(Guid planId) : base($"Current user is not allowed to change plan with Id: {planId}")
    {
        PlanId = planId;
    }
}