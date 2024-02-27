using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class TravelPlanNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelPlanNotFoundException(Guid id) : base($"Travel plan with Id: {id} was not found")
    {
        Id = id;
    }
}