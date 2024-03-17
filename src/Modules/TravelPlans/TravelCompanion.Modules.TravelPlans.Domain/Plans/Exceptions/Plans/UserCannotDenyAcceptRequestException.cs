using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class UserCannotDenyAcceptRequestException : TravelCompanionException
{
    public Guid UserId { get; set; }
    public UserCannotDenyAcceptRequestException(Guid id) : base($"User with Id: {id} cannot deny given accept request.")
    {
        UserId = id;
    }
}