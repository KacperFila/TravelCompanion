using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public class TravelPointRemoveRequest : IAuditable
{
    public Guid RequestId { get; private set; }
    public AggregateId TravelPointId { get; private set; }
    public EntityId SuggestedById { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public TravelPointRemoveRequest(AggregateId travelPointId, EntityId suggestedById)
    {
        RequestId = Guid.NewGuid();
        TravelPointId = travelPointId;
        SuggestedById = suggestedById;
    }

    public static TravelPointRemoveRequest Create(AggregateId travelPointId, EntityId suggestedBy)
    {
        return new TravelPointRemoveRequest(travelPointId, suggestedBy);
    }
}