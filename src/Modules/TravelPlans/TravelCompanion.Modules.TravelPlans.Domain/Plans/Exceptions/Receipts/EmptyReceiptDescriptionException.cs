using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class EmptyReceiptDescriptionException : TravelCompanionException
{
    public EmptyReceiptDescriptionException() : base("Given Receipt defines empty description.")
    {
    }
}
