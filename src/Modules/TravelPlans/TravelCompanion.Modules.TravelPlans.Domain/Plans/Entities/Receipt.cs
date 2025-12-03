using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Receipt : IAuditable
{
    public ReceiptId Id { get; private set; }
    public OwnerId? ReceiptOwnerId { get; private set; }
    public List<Guid> ReceiptParticipants { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public AggregateId? PlanId { get; private set; }
    public AggregateId? PointId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public Receipt(AggregateId? planId, AggregateId? pointId)
    {
        Id = Guid.NewGuid();
        ReceiptParticipants = new List<Guid>();
        Amount = Money.Create(0);
        PlanId = planId;
        PointId = pointId;
    }
    public static Receipt Create(OwnerId receiptOwnerId, List<Guid> receiptParticipants, Money amount, AggregateId? planId, AggregateId? pointId, string description)
    {
        if (!ValidPlanIdAndPointId(planId, pointId))
        {
            throw new InvalidReceiptParametersException();
        }

        var receipt = new Receipt(planId, pointId);
        receipt.ChangeReceiptParticipants(receiptParticipants);
        receipt.AddReceiptOwner(receiptOwnerId);
        receipt.ChangeAmount(amount);
        receipt.ChangeDescription(description);

        return receipt;
    }

    public void ChangeReceiptParticipants(List<Guid> receiptParticipants)
    {
        if (!receiptParticipants.Any())
        {
            throw new InvalidReceiptParametersException();
        }

        ReceiptParticipants = receiptParticipants;
    }

    public void AddReceiptOwner(Guid receiptOwnerId)
    {
        if (!ReceiptParticipants.Contains(receiptOwnerId))
        {
            throw new ReceiptNotFoundException(Id);
        }

        ReceiptOwnerId = receiptOwnerId;
    }

    public void AddReceiptParticipant(Guid participantId)
    {
        if (ReceiptParticipants.Contains(participantId))
        {
            throw new ParticipantAlreadyAddedException(participantId);
        }

        ReceiptParticipants.Add(participantId);
    }

    public void ChangeAmount(Money amount)
    {
        Amount = Money.Create(amount.Amount);
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrEmpty(description))
        {
            throw new EmptyReceiptDescriptionException();
        }

        Description = description;
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