using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions
{
    internal class InvalidVerificationTokenException : TravelCompanionException
    {
        public InvalidVerificationTokenException() : base("Invalid token.")
        {
        }
    }
}
