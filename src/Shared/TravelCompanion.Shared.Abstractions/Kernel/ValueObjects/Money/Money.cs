﻿using System.Collections.Generic;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money.Exceptions;

namespace TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

public class Money
{
    private static readonly HashSet<string> AllowedValues = new() { "PLN" };
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    private Money()
    {
    }
    private Money(decimal amount)
    {
        if (amount is < 0 or > 1000000)
            throw new InvalidAmountException(amount);

        Amount = amount;
        Currency = "PLN";
    }

    public static Money Create(decimal amount)
    {
        return new Money(amount);
    }
}
