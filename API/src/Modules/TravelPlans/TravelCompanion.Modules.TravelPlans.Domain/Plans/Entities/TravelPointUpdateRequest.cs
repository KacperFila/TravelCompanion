using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class TravelPointUpdateRequest : IAuditable
{
    public TravelPointUpdateRequestId RequestId { get; private set; }
    public AggregateId TravelPlanPointId { get; private set; }
    public EntityId SuggestedById { get; private set; }
    public string PlaceName { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public TravelPointUpdateRequest(AggregateId travelPlanPointId, EntityId suggestedById)
    {
        RequestId = Guid.NewGuid();
        TravelPlanPointId = travelPlanPointId;
        SuggestedById = suggestedById;
    }

    public static TravelPointUpdateRequest Create(AggregateId travelPlanPointId, EntityId suggestedById, string PlaceName)
    {
        var request = new TravelPointUpdateRequest(travelPlanPointId, suggestedById);
        request.PlaceName = PlaceName;

        return request;
    }

    public void ChangePlaceName(string placeName)
    {
        if (string.IsNullOrEmpty(placeName))
        {
            throw new EmptyTravelPointPlaceNameException(TravelPlanPointId);
        }

        PlaceName = placeName;
    }
}