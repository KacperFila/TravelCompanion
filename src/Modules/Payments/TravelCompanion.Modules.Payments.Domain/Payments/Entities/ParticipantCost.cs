using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Entities;

public class ParticipantCost
{
    public Guid Id { get; set; }
    public AggregateId SummaryId { get; set; }
    public Guid ParticipantId { get; private set; }
    public Money Value { get; private set; }

    private static ParticipantCost Create(Guid summaryId, Guid participantId, Money value)
    {
        return new ParticipantCost()
        {
            SummaryId = summaryId,
            ParticipantId = participantId,
            Value = value
        };
    }
}