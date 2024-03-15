using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public class TravelPoint
{
    public Guid TravelPointId { get; set; }
    public Guid TravelId { get; private set; }
    public string PlaceName { get; private set; }
    public List<Receipt> Receipts { get; private set; }
    public Money TotalCost { get; private set; }
    public bool IsVisited { get; private set; }

    public TravelPoint()
    {
        
    }
    public TravelPoint(Guid id, string placeName, Guid travelId, List<Receipt> receipts, Money totalCost)
    {
        TravelPointId = id;
        TravelId = travelId;
        PlaceName = placeName;
        Receipts = receipts;
        TotalCost = totalCost;
        IsVisited = false;
    }

    public void VisitTravelPoint()
    {
        IsVisited = true;
    }
}