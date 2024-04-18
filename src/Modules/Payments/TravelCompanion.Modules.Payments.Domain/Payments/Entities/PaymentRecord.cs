using TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Entities;

public class PaymentRecord
{
    public Guid Id { get; private set; }
    public Guid PayerId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public Money Amount { get; private set; }
    public string Title { get; private set; }

    public PaymentRecord(Guid id)
    {
        Id = id;
    }

    public static PaymentRecord Create(Guid payerId, Guid receiverId, string title)
    {
        var record = new PaymentRecord(Guid.NewGuid());
        record.ChangePayer(payerId);
        record.ChangeReceiver(receiverId);
        record.ChangeTitle(title);

        return record;
    }

    public static PaymentRecord Create(Guid payerId, Guid receiverId, Money amount, List<Guid> receiptParticipants, string title)
    {
        var record = new PaymentRecord(Guid.NewGuid());
        record.ChangePayer(payerId);
        record.ChangeReceiver(receiverId);
        record.ChangeTitle(title);
        record.ChangeAmount(amount, receiptParticipants);

        return record;
    }

    public void ChangeAmount(Money amount, List<Guid> receiptParticipants)
    {
        if (!receiptParticipants.Any())
        {
            throw new InvalidListOfReceiptParticipantsException();
        }

        Amount = Money.Create(amount.Amount / receiptParticipants.Count);
    }

    public void ChangePayer(Guid payerId)
    {
        PayerId = payerId;
    }

    public void ChangeReceiver(Guid receiverId)
    {
        ReceiverId = receiverId;
    }

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new InvalidTitleException();
        }

        Title = title;
    }
}