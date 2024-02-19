using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class TravelCannotBeDeletedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelCannotBeDeletedException(Guid id) : base($"Travel with Id: {id} could not be deleted.")
    {
        Id = id;
    }
}