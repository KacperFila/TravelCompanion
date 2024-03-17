using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class PlanNotDuringPlanningException : TravelCompanionException
{
    public Guid Id { get; set; }
    public PlanNotDuringPlanningException(Guid id) : base($"Status of plan with Id : {id} is not during planning.")
    {
        Id = id;
    }
}