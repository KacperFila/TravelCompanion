using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Entities;

public class Receipt
{
    public Guid Id { get; set; }
    public List<Guid> ParticipantsIds { get; private set; }
    public Money Value { get; private set; }

    private static Receipt Create(List<Guid> participantId, Money value)
    {
        return new Receipt()
        {
            ParticipantsIds = participantId,
            Value = value
        };
    }
}