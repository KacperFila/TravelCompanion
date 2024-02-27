﻿using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class UserNotAllowedToChangeTravelPointException : TravelCompanionException
{
    public UserNotAllowedToChangeTravelPointException() : base("User not allowed to change given travel point")
    {
    }
}