using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class AcceptPlanRequestForPlanNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public AcceptPlanRequestForPlanNotFoundException(Guid planId) : base($"Accept plan request for plan with Id: {planId} was not found.")
    {
        Id = planId;
    }
}