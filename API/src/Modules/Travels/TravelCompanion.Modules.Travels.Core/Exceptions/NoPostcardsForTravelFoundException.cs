using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class NoPostcardsForTravelFoundException : TravelCompanionException
{
    public Guid TravelId { get; set; }
    public NoPostcardsForTravelFoundException(Guid travelId) : base($"No postcards for travel with Id: {travelId} found.")
    {
        TravelId = travelId;
    }
}