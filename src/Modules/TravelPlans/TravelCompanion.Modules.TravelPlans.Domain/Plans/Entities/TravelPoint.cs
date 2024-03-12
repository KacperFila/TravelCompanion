using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public class TravelPoint : AggregateRoot
{
    public AggregateId PlanId { get; private set; }
    public string PlaceName { get; private set; }
    public bool IsAccepted { get; private set; }
    public List<Receipt> Receipts { get; private set; }
    public Money TotalCost { get; private set; }

    public TravelPoint(AggregateId id, string placeName, AggregateId planId, bool isAccepted, int version = 0)
    {
        Id = id;
        IsAccepted = isAccepted;
        PlanId = planId;
        ChangeTravelPointPlaceName(placeName);
        Receipts = new List<Receipt>();
        TotalCost = Money.Create(0);
        Version = version;
    }

    public static TravelPoint Create(Guid id, string placeName, AggregateId travelPlanId, bool isAccepted)
    {
        var travelPoint = new TravelPoint(id, placeName, travelPlanId, isAccepted);
        travelPoint.ClearEvents();
        travelPoint.Version = 0;

        return travelPoint;
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
    
    public void AddReceipt(Guid pointId, decimal amount, List<Guid> receiptParticipants, string description)
    {
        var receipt = Receipt.Create(receiptParticipants, Money.Create(amount), null, pointId, description);
        Receipts.Add(receipt);
        CalculateCost();
        IncrementVersion();
    }

    public void RemoveReceipt(Receipt receipt)
    {
        Receipts.Remove(receipt);
        CalculateCost();
        IncrementVersion();
    }
    private void CalculateCost()
    {
        var amountSum = Receipts.Sum(x => x.Amount.Amount);
        TotalCost = Money.Create(amountSum);
    }
}