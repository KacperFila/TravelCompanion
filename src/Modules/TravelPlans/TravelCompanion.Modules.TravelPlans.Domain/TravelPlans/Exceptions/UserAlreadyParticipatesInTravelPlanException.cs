using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class UserAlreadyParticipatesInTravelPlanException : TravelCompanionException
{
    public Guid UserId { get; set; }
    public UserAlreadyParticipatesInTravelPlanException(Guid userId) : base($"User with Id: {userId} already participates in given travel plan.")
    {
        UserId = userId;
    }
}