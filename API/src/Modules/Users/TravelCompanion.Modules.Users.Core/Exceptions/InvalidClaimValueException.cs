using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions;

internal class InvalidClaimValueException : TravelCompanionException
{
    public InvalidClaimValueException(string claimName) : base($"Invalid claim: {claimName} value.")
    {
    }
}