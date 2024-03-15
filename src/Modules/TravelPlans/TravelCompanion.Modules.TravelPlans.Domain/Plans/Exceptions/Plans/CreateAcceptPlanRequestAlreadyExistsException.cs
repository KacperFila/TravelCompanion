using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class CreateAcceptPlanRequestAlreadyExistsException : TravelCompanionException
{
    public Guid Id { get; set; }
    public CreateAcceptPlanRequestAlreadyExistsException(Guid id) : base($"Accept Plan Request for plan with Id: {id} already exists.")
    {
        Id = id;
    }
}