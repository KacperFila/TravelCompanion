using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public sealed class NoActivePlanForUserException : TravelCompanionException
{
    public Guid UserId { get; set; }
    public NoActivePlanForUserException(Guid userId) : base($"No active plan found for user: {userId}")
    {
        UserId = userId;
    }
}
