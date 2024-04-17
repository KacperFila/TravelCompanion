using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Entities;

public class ParticipantCost
{
    public Guid Id { get; set; }
    public AggregateId SummaryId { get; set; }
    public List<Guid> ParticipantsIds { get; private set; }
    public Money Value { get; private set; }

    private static ParticipantCost Create(Guid summaryId, List<Guid> participantId, Money value)
    {
        return new ParticipantCost()
        {
            SummaryId = summaryId,
            ParticipantsIds = participantId,
            Value = value
        };
    }
}