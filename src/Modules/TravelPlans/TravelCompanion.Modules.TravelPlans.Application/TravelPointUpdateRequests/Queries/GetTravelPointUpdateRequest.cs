using TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;
using TravelCompanion.Shared.Abstractions.Queries;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Queries;

public class GetTravelPointUpdateRequest : IQuery<List<UpdateRequestDto>>
{
    public Guid PointId { get; set; }
}