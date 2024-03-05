using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Receipt
{
    public ReceiptId Id { get; private set; }
    public ParticipantId ParticipantId { get; private set; }
    public Money Amount { get; private set; }
    public AggregateId? PlanId { get; private set; }
    public AggregateId? PointId { get; private set; }

    public Receipt(ParticipantId participantId, AggregateId? planId, AggregateId? pointId)
    {
        Id = Guid.NewGuid();
        ParticipantId = participantId;
        Amount = Money.Create(0);
        PlanId = planId;
        PointId = pointId;
    }

    public void ChangeParticipantId(ParticipantId participantId)
    {
        // TODO check for default value
        ParticipantId = participantId;
    }

    public void ChangeAmount(Money amount)
    {
        Amount = Money.Create(amount.Amount);
    }

    public static Receipt Create(ParticipantId participantId, Money amount, AggregateId? planId, AggregateId? pointId)
    {
        if (!ValidPlanIdAndPointId(planId, pointId))
        {
            throw new InvalidReceiptParameteresException();
        }

        var receipt = new Receipt(participantId, planId, pointId);
        receipt.ChangeParticipantId(participantId); // change participantId potrzebne tu?
        receipt.ChangeAmount(amount);


        return receipt;
    }

    private static bool ValidPlanIdAndPointId(AggregateId? planId, AggregateId? pointId)
    {
        if (planId is null && pointId is null)
        {
            return false;
        }

        if (planId is not null && pointId is not null)
        {
            return false;
        }

        return true;
    }
}