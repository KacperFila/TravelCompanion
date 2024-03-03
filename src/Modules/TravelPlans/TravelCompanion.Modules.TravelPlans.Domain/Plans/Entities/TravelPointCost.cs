using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class TravelPointCost
{
    public EntityId Id { get; private set; }
    public AggregateId TravelPointId { get; private set; }
    public TravelPoint TravelPoint { get; private set; }
    public List<Receipt> Receipts { get; private set; }
    public Money OverallCost { get; private set; }


    public TravelPointCost(AggregateId travelPointId)
    {
        Id = Guid.NewGuid();
        TravelPointId = travelPointId; 
        Receipts = new List<Receipt>();
        OverallCost = Money.Create(0);
    }

    public static TravelPointCost Create(AggregateId travelPointId)
    {
        return new TravelPointCost(travelPointId);
    }

    public void AddReceipt(ParticipantId participantId, Money amount)
    {
        Receipts.Add(Receipt.Create(participantId, amount, Id));
        CalculateOverallCost();
    }

    public void RemoveReceipt(ParticipantId participantId, Money amount)
    {
        var receipt = Receipts.SingleOrDefault(x => x.ParticipantId == participantId && x.Amount == amount);
        if(receipt is null)
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