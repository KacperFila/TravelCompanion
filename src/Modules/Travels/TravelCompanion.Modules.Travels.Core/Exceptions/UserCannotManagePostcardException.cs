using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class UserCannotManagePostcardException : TravelCompanionException
{
    public Guid Id { get; set; }
    public UserCannotManagePostcardException(Guid id) : base($"Current user cannot manage postcard with Id: {id}.")
    {
        Id = id;
    }
}