using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Queries;

public class GetUserPlanTotalCost : IQuery<Money>
{
    public Guid planId { get; set; }
}