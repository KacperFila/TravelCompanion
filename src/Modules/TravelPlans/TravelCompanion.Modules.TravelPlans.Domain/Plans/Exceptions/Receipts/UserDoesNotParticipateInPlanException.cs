using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class UserDoesNotParticipateInPlanException : TravelCompanionException
{
    public Guid PlanId { get; set; }
    public Guid UserId { get; set; }
    public UserDoesNotParticipateInPlanException(Guid userId, Guid planId) : base($"User with Id: {userId} does not participate in plan with Id: {planId}")
    {
        PlanId = planId;
        UserId = userId;
    }
}