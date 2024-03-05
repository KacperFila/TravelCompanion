﻿using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class InvalidReceiptParameteresException : TravelCompanionException
{
    public InvalidReceiptParameteresException() : base("Given receipt defines invalid parameters")
    {
    }
}