using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions
{
    public class UserNotActiveException : TravelCompanionException
    {
        public UserNotActiveException() : base($"User is not active.")
        {
        }
    }
}