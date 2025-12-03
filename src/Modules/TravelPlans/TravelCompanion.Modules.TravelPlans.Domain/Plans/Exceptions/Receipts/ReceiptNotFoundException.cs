using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class ReceiptNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public ReceiptNotFoundException(Guid id) : base($"Receipt with Id: {id} was not found.")
    {
        Id = id;
    }
}