using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries;
public class GetUserActivePlan : IQuery<PlanDetailsDTO>
{
    public Guid userId { get; set; }
}