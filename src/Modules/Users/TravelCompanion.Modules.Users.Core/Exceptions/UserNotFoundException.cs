using System;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions;

public sealed class UserNotFoundException : TravelCompanionException
{
    public Guid UserId { get; set; }
    public UserNotFoundException(Guid userId) : base($"User with Id: {userId} was not found.")
    {
        UserId = userId;
    }
}
