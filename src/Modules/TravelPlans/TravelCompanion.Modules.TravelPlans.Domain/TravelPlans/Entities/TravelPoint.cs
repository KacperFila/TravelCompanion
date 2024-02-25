using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

public class TravelPoint : AggregateRoot
{
    public string PlaceName { get; private set; }
    public bool IsAccepted { get; private set; }
    public TravelPoint(AggregateId id, string placeName)
    {
        Id = id;
        IsAccepted = false;
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

    public void AcceptTravelPoint(AggregateId id)
    {
        IsAccepted = true;
        IncrementVersion();
    }

    public static TravelPoint Create(Guid id, string placeName)
    {
        var travelPoint = new TravelPoint(id, placeName);
        travelPoint.ClearEvents();
        travelPoint.Version = 0;
        //travelPoint.AddEvent(new TravelPlanTravelPointAdded(travelPoint));

        return travelPoint;
    }
}