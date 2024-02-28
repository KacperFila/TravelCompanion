using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class PlanNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public PlanNotFoundException(Guid id) : base($"Travel plan with Id: {id} was not found")
    {
        Id = id;
    }
}