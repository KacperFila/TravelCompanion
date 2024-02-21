using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class TravelDoesNotBelongToUserException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelDoesNotBelongToUserException(Guid id) : base($"Travel with Id: {id} does not belong to current user.")
    {
        Id = id;
    }
}