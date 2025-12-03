using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class InvalidReceiptParameteresException : TravelCompanionException
{
    public InvalidReceiptParameteresException() : base("Given receipt defines invalid parameters")
    {
    }
}