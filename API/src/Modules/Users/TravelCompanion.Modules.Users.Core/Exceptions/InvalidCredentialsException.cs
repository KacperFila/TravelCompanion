using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions
{
    internal class InvalidCredentialsException : TravelCompanionException
    {
        public InvalidCredentialsException() : base("Given email or password are incorrect!")
        {
        }
    }
}
