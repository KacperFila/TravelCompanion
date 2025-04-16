using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Queries;

public class GetTravelPointUpdateRequest : IQuery<List<TravelPointUpdateRequest>>
{
    public Guid PlanId { get; set; }
}