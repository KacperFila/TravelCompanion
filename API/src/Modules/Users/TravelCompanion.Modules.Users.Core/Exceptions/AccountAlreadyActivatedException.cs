using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions;

public class AccountAlreadyActivatedException : TravelCompanionException
{
    public AccountAlreadyActivatedException() : base($"Given account is already active.")
    {
    }
}