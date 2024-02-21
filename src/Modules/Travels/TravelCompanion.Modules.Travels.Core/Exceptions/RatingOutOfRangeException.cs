using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class RatingOutOfRangeException : TravelCompanionException
{
    public RatingOutOfRangeException() : base("Travel rating out of range.")
    {
    }
}