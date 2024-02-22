using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class UserCannotManagePostcardException : TravelCompanionException
{
    public Guid Id { get; set; }
    public UserCannotManagePostcardException(Guid postcardId) : base($"Current user cannot manage postcard with Id: {postcardId}.")
    {
        Id = postcardId;
    }
}