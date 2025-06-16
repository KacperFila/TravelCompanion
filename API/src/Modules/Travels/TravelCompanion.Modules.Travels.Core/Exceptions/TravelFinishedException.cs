using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class TravelFinishedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelFinishedException(Guid travelId) : base($"Travel with Id: {travelId} has been already finished.")
    {
        Id = travelId;
    }
}