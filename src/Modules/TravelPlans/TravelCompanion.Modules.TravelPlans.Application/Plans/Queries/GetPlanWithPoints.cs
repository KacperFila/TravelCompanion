using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries;

public class GetPlanWithPoints : IQuery<PlanDetailsDTO>
{
    public Guid planId { get; set; }
}