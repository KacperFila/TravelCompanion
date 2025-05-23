using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;

public class UserNotOwnerOfPlanException : TravelCompanionException
{
    public Guid UserId { get; set; }
    public UserNotOwnerOfPlanException(Guid userId) : base($"User with Id: {userId} is not owner of given plan.")
    {
        this.UserId = userId;
    }
}