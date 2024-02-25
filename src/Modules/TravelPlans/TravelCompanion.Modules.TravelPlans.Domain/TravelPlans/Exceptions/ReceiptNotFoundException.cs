using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class ReceiptNotFoundException : TravelCompanionException
{
    public ReceiptNotFoundException() : base("Receipt not found exception")
    {
    }
}