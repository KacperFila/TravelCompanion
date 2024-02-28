using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class TravelPointUpdateRequestNotFoundException : TravelCompanionException
{
    public Guid Id;
    public TravelPointUpdateRequestNotFoundException(Guid id) : base($"Travel point update request with Id: {id} was not found.")
    {
        Id = id;
    }
}