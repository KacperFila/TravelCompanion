using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class PostcardNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public PostcardNotFoundException(Guid id) : base($"Postcard with Id: {id} was not found.")
    {
        Id = id;
    }
}