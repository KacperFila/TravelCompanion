using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class UserAlreadyParticipatesInPlanException : TravelCompanionException
{
    public Guid UserId { get; set; }
    public UserAlreadyParticipatesInPlanException(Guid userId) : base($"User with Id: {userId} already participates in given travel plan.")
    {
        UserId = userId;
    }
}