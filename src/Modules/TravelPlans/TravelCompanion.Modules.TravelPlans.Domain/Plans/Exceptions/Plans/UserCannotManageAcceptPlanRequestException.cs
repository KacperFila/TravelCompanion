using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class UserCannotManageAcceptPlanRequestException : TravelCompanionException
{
    public Guid Id { get; set; }
    public UserCannotManageAcceptPlanRequestException(Guid id) : base($"User with Id: {id} cannot manage given accept plan request.")
    {
        Id = id;
    }
}