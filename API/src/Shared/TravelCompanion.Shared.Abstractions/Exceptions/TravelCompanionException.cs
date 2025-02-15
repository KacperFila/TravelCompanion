using System;

namespace TravelCompanion.Shared.Abstractions.Exceptions
{
    public abstract class TravelCompanionException : Exception
    {
        protected TravelCompanionException(string message) : base(message)
        {
            
        }
    }
}
