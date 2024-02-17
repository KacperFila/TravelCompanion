using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions
{
    internal class EmailInUseException : TravelCompanionException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}
