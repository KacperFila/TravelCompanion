using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.DTO;

internal class UpdateRequestUpdateResponse
{
    public IEnumerable<TravelPointUpdateRequest> UpdateRequests { get; set; }
    public AggregateId PointId { get; set; }
}
