using TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Entities;

public class PaymentRecord
{
    public Guid Id { get; private set; }
    public Guid PayerId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string Title { get; private set; }

    public PaymentRecord(Guid id)
    {
        Id = id;
    }

    private static PaymentRecord Create(Guid payerId, Guid receiverId, string title)
    {
        var record = new PaymentRecord(Guid.NewGuid());
        record.ChangePayer(payerId);
        record.ChangeReceiver(receiverId);
        record.ChangeTitle(title);

        return record;
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