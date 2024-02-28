using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class TravelPointCost : AggregateRoot
{
    public TravelPointId TravelPointId { get; private set; }
    public List<Receipt> Receipts { get; private set; }
    public Money OverallCost { get; private set; }


    public TravelPointCost(AggregateId id, TravelPointId travelPointId, List<Receipt> receipts, Money overallCost)
    : this(id, travelPointId)
    {
        Receipts = receipts;
        OverallCost = overallCost;
    }

    public TravelPointCost(AggregateId id, TravelPointId travelPointId)
    {
        Id = id;
        TravelPointId = travelPointId;
    }

    public static TravelPointCost Create(AggregateId id, TravelPointId travelPointId)
    {
        var travelPointCost = new TravelPointCost(id, travelPointId, new List<Receipt>(), Money.Create(0));
        travelPointCost.ClearEvents();
        travelPointCost.Version = 0;

        return travelPointCost;
    }

    public void AddReceipt(ParticipantId participantId, Money amount)
    {
        Receipts.Add(Receipt.Create(participantId, amount));
        CalculateOverallCost();
    }

    public void RemoveReceipt(ParticipantId participantId, Money amount)
    {
        // walidacja
        var receipt = Receipts.SingleOrDefault(x => x.ParticipantId == participantId && x.Amount == amount);
        if (receipt is null)
        {
            throw new ReceiptNotFoundException();
        }
        Receipts.Remove(receipt);
        CalculateOverallCost();
    }

    private void CalculateOverallCost()
    {
        var total = Receipts.Sum(x => x.Amount.Amount);
        OverallCost = Money.Create(total);
    }
}