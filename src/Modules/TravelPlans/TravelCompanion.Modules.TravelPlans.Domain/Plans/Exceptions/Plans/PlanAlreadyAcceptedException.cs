using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class PlanAlreadyAcceptedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public PlanAlreadyAcceptedException(Guid id) : base($"Plan with Id: {id} is already accepted.")
    {
        Id = id;
    }
}