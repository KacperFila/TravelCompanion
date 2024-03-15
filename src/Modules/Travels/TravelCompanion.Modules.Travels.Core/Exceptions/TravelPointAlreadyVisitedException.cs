using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class TravelPointAlreadyVisitedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelPointAlreadyVisitedException(Guid id) : base($"Travel Point with Id: {id} has been already visited.")
    {
        Id = id;
    }
}