using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Invitations;

public class InvitationAlreadyExistsException : TravelCompanionException
{
    public Guid TravelPlanId { get; set; }
    public Guid UserId { get; set; }
    public InvitationAlreadyExistsException(Guid travelPlanId, Guid userId) : base($"User with Id: {userId} is already invitated to travel plan with Id: {travelPlanId}.")
    {
        TravelPlanId = travelPlanId;
        UserId = userId;
    }
}