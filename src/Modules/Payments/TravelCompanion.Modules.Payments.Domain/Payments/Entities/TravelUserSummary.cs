using TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Entities;

public class TravelUserSummary : AggregateRoot, IAuditable
{
    public List<PaymentRecord> Payments { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public TravelUserSummary(AggregateId id)
    {
        Id = id;
    }

    public static TravelUserSummary Create(List<PaymentRecord> payments)
    {
        var summary = new TravelUserSummary(Guid.NewGuid());
        summary.ChangePayments(payments);

        return summary;
    }

    public void ChangePayments(List<PaymentRecord> payments)
    {
        if (!payments.Any())
        {
            throw new InvalidPaymentsException();
        }

        Payments = payments;
    }
}