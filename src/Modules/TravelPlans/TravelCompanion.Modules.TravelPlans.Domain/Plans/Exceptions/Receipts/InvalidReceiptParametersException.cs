using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class InvalidReceiptParametersException : TravelCompanionException
{
    public InvalidReceiptParametersException() : base("Given receipt defines invalid parameters")
    {
    }
}