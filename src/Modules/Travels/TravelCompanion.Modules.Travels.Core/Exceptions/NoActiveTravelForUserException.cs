using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class NoActiveTravelForUserException : TravelCompanionException
{
    public Guid UserId { get; }
    
    public NoActiveTravelForUserException(Guid userId) : base($"No active travel for user: {userId}")
    {
        UserId = userId;
    }
}