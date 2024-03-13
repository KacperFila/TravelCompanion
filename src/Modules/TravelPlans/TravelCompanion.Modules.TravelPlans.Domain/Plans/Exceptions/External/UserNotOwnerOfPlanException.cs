using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.External;

public class UserNotOwnerOfPlanException : TravelCompanionException
{
    public Guid Id { get; set; }
    public UserNotOwnerOfPlanException(Guid id) : base($"User with Id: {id} is not owner af given plan.")
    {
        Id = id;
    }
}