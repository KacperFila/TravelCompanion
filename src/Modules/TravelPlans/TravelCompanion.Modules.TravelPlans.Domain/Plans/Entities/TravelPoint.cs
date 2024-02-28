using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public class TravelPoint : AggregateRoot
{
    public AggregateId TravelPlanId { get; private set; }
    public string PlaceName { get; private set; }
    public bool IsAccepted { get; private set; }
    public TravelPoint(AggregateId id, string placeName, AggregateId travelPlanId)
    {
        Id = id;
        IsAccepted = false;
        TravelPlanId = travelPlanId;
        ChangeTravelPointPlaceName(placeName);
    }

    public void ChangeTravelPointPlaceName(string placeName)
    {
        if (string.IsNullOrEmpty(placeName))
        {
            throw new EmptyTravelPointPlaceNameException(Id);
        }

        PlaceName = placeName;
        IncrementVersion();
    }

    public void AcceptTravelPoint()
    {
        IsAccepted = true;
        IncrementVersion();
    }

    public static TravelPoint Create(Guid id, string placeName, AggregateId travelPlanId)
    {
        var travelPoint = new TravelPoint(id, placeName, travelPlanId);
        travelPoint.ClearEvents();
        travelPoint.Version = 0;
        //travelPoint.AddEvent(new TravelPlanTravelPointAdded(travelPoint));

        return travelPoint;
    }
}