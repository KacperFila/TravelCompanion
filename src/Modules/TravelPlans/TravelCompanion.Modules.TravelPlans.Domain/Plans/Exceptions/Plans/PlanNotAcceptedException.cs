using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class PlanNotAcceptedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public PlanNotAcceptedException(Guid id) : base($"Plan with Id: {id} is not accepted.")
    {
        Id = id;
    }
}