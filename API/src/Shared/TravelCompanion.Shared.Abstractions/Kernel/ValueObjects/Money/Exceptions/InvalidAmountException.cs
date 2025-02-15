using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money.Exceptions;

public class InvalidAmountException : TravelCompanionException
{
    public decimal Amount { get; set; }
    public InvalidAmountException(decimal amount) : base($"Given amount of money is incorrect: {amount}")
    {
        Amount = amount;
    }
}