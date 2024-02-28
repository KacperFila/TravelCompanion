using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Receipt
{
    public ReceiptId Id { get; private set; }
    public ParticipantId ParticipantId { get; private set; }
    public Money Amount { get; private set; }

    public Receipt(ParticipantId participantId)
    {
        Id = Guid.NewGuid();
        ParticipantId = participantId;
        Amount = Money.Create(0);
    }

    public void ChangeParticipantId(ParticipantId participantId) //TODO does it make sense?
    {
        ParticipantId = participantId;
    }

    public void ChangeAmount(Money amount)
    {
        Amount = Money.Create(amount.Amount);
    }

    public static Receipt Create(ParticipantId participantId, Money amount)
    {
        var receipt = new Receipt(participantId);
        receipt.ChangeParticipantId(participantId);
        receipt.ChangeAmount(amount);

        return receipt;
    }
}