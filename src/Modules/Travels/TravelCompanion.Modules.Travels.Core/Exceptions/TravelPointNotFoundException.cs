using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class TravelPointNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelPointNotFoundException(Guid id) : base($"Travel Point with Id: {id} was not found.")
    {
        Id = id;
    }
}