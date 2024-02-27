using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class UserNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public UserNotFoundException(Guid id) : base($"User with Id: {id} was not found")
    {
        Id = id;
    }
}