using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class PostcardCannotBeDeletedException : TravelCompanionException
{
    public Guid Id { get; set; }
    public PostcardCannotBeDeletedException(Guid postcardId) : base($"Postcard with Id: {postcardId} cannot not be deleted.")
    {
        Id = postcardId;
    }
}