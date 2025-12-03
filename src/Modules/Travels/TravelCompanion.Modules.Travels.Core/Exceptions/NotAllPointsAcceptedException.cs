using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class NotAllPointsAcceptedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public NotAllPointsAcceptedException(Guid travelId) : base($"Travel with Id: {travelId} cannot not be completed - not all travel points has been marked as visited.")
    {
        Id = travelId;
    }
}