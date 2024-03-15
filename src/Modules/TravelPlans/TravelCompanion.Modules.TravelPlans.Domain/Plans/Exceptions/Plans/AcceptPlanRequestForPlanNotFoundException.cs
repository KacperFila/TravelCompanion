using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class AcceptPlanRequestForPlanNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public AcceptPlanRequestForPlanNotFoundException(Guid id) : base($"Accept plan request for plan with Id: {id} was not found.")
    {
        Id = Id;
    }
}