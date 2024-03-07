using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class EmptyReceiptDescriptionException : TravelCompanionException
{
    public EmptyReceiptDescriptionException() : base("Given Receipt defines empty description.")
    {
    }
}
