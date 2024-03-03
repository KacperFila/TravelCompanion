using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Receipt
{
    public ReceiptId Id { get; private set; }
    public EntityId TravelPointCostId { get; private set; }
    public ParticipantId ParticipantId { get; private set; }
    public Money Amount { get; private set; }

    public Receipt(ParticipantId participantId, EntityId travelPointCostId)
    {
        Id = Guid.NewGuid();
        ParticipantId = participantId;
        Amount = Money.Create(0);
        TravelPointCostId = travelPointCostId;
    }

    public void ChangeParticipantId(ParticipantId participantId)
    {
        ParticipantId = participantId;
    }

    public void ChangeAmount(Money amount)
    {
        Amount = Money.Create(amount.Amount);
    }

    public static Receipt Create(ParticipantId participantId, Money amount, EntityId travelPointCostId)
    {
        var receipt = new Receipt(participantId, travelPointCostId);
        receipt.ChangeParticipantId(participantId); // change participantId potrzebne tu?
        receipt.ChangeAmount(amount);

        return receipt;
    }
}