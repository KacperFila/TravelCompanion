using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class TravelNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelNotFoundException(Guid id) : base($"Travel with Id: {id} was not found.")
    {
        Id = id;
    }
}