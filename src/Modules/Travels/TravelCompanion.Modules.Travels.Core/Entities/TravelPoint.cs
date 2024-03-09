using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public class TravelPoint
{
    public Guid PlanId { get; private set; }
    public string PlaceName { get; private set; }
    public List<Receipt> Receipts { get; private set; }
    public Money TotalCost { get; private set; }

    public TravelPoint()
    {
        
    }

    public TravelPoint(string placeName, AggregateId planId, List<Receipt> receipts, Money totalCost)
    {
        PlanId = planId;
        PlaceName = placeName;
        Receipts = receipts;
        TotalCost = totalCost;
    }
}