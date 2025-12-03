using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class TravelPointNotVisitedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelPointNotVisitedException(Guid id) : base($"Travel Point with Id: {id} is not marked as visited.")
    {
        Id = id;
    }
}