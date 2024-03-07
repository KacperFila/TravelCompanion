using TravelCompanion.Modules.Travels.Core.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.Entities;

public sealed class Receipt
{
    public Guid Id { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; }
    public Guid TravelId { get; private set; }

    public Receipt(Guid travelId)
    {
        Id = Guid.NewGuid();
        TravelId = travelId;
        Amount = Money.Create(0);
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

    public static Receipt Create(Guid travelId, Money amount, string description)
    {
        var receipt = new Receipt(travelId);
        receipt.ChangeAmount(amount);
        receipt.ChangeDescription(description);

        return receipt;
    }
}