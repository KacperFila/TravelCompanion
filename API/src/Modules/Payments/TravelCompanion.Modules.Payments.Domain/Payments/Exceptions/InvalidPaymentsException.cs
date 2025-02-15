using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;

public class InvalidPaymentsException : TravelCompanionException
{
    public InvalidPaymentsException() : base($"Summary defines invalid list of payments.")
    {
    }
}