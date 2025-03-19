using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using EmptyReceiptDescriptionException = TravelCompanion.Modules.Travels.Core.Exceptions.EmptyReceiptDescriptionException;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public sealed class Receipt : IAuditable
{
    public Guid Id { get; private set; }
    public Guid? ReceiptOwnerId { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public Guid? TravelId { get; private set; }
    public Guid? TravelPointId { get; private set; }
    public List<Guid> ReceiptParticipants { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Receipt(string description, Guid? travelId, Guid? travelPointId)
    {
        Id = Guid.NewGuid();
        Amount = Money.Create(0);
        Description = description;
        TravelId = travelId;
        TravelPointId = travelPointId;
    }

    public static Receipt Create(Guid receiptOwnerId, Guid? travelId, Guid? travelPointId, Money amount, string description, List<Guid> receiptParticipants)
    {
        if (!ValidTravelIdAndPointId(travelId, travelPointId))
        {
            throw new InvalidReceiptParameteresException();
        }
        var receipt = new Receipt(description, travelId, travelPointId);
        receipt.AddReceiptOwner(receiptOwnerId);
        receipt.ChangeReceiptParticipants(receiptParticipants);
        receipt.ChangeAmount(amount);
        receipt.ChangeDescription(description);

        return receipt;
    }

    public void AddReceiptOwner(Guid receiptOwnerId)
    {
        if (!ReceiptParticipants.Contains(receiptOwnerId))
        {
            throw new ReceiptNotFoundException(Id);
        }

        ReceiptOwnerId = receiptOwnerId;
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

    public void ChangeReceiptParticipants(List<Guid> receiptParticipants)
    {
        if (!receiptParticipants.Any())
        {
            throw new InvalidListOfReceiptParticipantsException();
        }

        ReceiptParticipants = receiptParticipants;
    }



    private static bool ValidTravelIdAndPointId(Guid? travelId, Guid? pointId)
    {
        if (travelId is null && pointId is null)
        {
            return false;
        }

        if (travelId is not null && pointId is not null)
        {
            return false;
        }

        return true;
    }
}