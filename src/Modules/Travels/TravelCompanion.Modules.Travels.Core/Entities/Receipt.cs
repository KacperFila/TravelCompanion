using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public sealed class Receipt
{
    public Guid Id { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public Guid? TravelId { get; private set; }
    public Guid? TravelPointId { get; private set; }

    public Receipt(string description, Guid? travelId, Guid? travelPointId)
    {
        Id = Guid.NewGuid();
        Amount = Money.Create(0);
        Description = description;
        TravelId = travelId;
        TravelPointId = travelPointId;
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

    public static Receipt Create(Guid? travelId, Guid? travelPointId, Money amount, string description)
    {
        if (!ValidTravelIdAndPointId(travelId, travelPointId))
        {
            throw new InvalidReceiptParameteresException();
        }

        var receipt = new Receipt(description, travelId, travelPointId);
        receipt.ChangeAmount(amount);
        receipt.ChangeDescription(description);

        return receipt;
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