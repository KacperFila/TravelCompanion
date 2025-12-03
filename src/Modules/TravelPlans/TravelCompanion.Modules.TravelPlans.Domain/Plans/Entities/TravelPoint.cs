using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public class TravelPoint : AggregateRoot, IAuditable
{
    public AggregateId PlanId { get; private set; }
    public string PlaceName { get; private set; }
    public bool IsAccepted { get; private set; }
    public List<Receipt> Receipts { get; private set; }
    public Money TotalCost { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public int TravelPlanOrderNumber { get; private set; }
    private TravelPoint(AggregateId id, string placeName, AggregateId planId, bool isAccepted, int version = 0)
    {
        Id = id;
        IsAccepted = isAccepted;
        PlanId = planId;
        ChangeTravelPointPlaceName(placeName);
        Receipts = new List<Receipt>();
        TotalCost = Money.Create(0);
        Version = version;
    }

    public static TravelPoint Create(Guid id, string placeName, AggregateId travelPlanId, bool isAccepted, int orderNumber)
    {
        var travelPoint = new TravelPoint(id, placeName, travelPlanId, isAccepted);
        travelPoint.ChangeTravelPlanOrderNumber(orderNumber);
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

    public void ChangeTravelPlanOrderNumber(int orderNumber)
    {
        if (orderNumber < 0)
        {
            throw new InvalidPointOrderException(Id);
        }

        TravelPlanOrderNumber = orderNumber;
        IncrementVersion();
    }

    public void DecreaseTravelPlanOrderNumber()
    {
        TravelPlanOrderNumber--;
        IncrementVersion();
    }

    public void AcceptTravelPoint()
    {
        if (IsAccepted)
        {
            throw new PointAlreadyAcceptedException(Id);
        }

        IsAccepted = true;
        IncrementVersion();
    }

    public void AddReceipt(Receipt receipt)
    {
        Receipts.Add(receipt);
        CalculateCost();
        IncrementVersion();
    }

    public void RemoveReceipt(Receipt receipt)
    {
        if (!Receipts.Contains(receipt))
        {
            throw new ReceiptNotFoundException(receipt.Id);
        }

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