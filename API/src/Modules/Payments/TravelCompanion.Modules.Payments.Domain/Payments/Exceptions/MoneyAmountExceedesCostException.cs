using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;

public class MoneyAmountExceedesCostException : TravelCompanionException
{
    public MoneyAmountExceedesCostException() : base($"Given amount exceedes current cost value.")
    {
    }
}