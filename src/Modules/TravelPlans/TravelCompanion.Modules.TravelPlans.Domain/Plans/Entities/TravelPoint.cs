using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
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

    public TravelPoint(AggregateId id, string placeName, AggregateId planId)
    {
        Id = id;
        IsAccepted = false;
        PlanId = planId;
        ChangeTravelPointPlaceName(placeName);
        Receipts = new List<Receipt>();
        TotalCost = Money.Create(0);
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

        return travelPoint;
    }

    public void AddReceipt(Guid pointId, decimal amount, List<EntityId> receiptParticipants)
    {
        var receipt = Receipt.Create(receiptParticipants, Money.Create(amount), null, pointId);
        Receipts.Add(receipt);
    }

    public void CalculateCost()
    {
        var amountSum = Receipts.Sum(x => x.Amount.Amount);
        TotalCost = Money.Create(amountSum);
    }
}