using TravelCompanion.Modules.TravelPlans.Application.Plans.DTO;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries;

public class GetUserPlans: PagedQueryGeneric<PlanDetailsDTO>
{
    public GetUserPlans()
    {
        SortOrder = "DESC";
        OrderBy = "Id";
    }
}