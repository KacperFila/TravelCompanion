using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;

public class InvalidTitleException : TravelCompanionException
{
    public InvalidTitleException() : base($"Given receipt defines invalid title.")
    {
    }
}