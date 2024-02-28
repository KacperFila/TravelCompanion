using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class TravelPointNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelPointNotFoundException(Guid id) : base($"Travel point with Id: {id} was not found")
    {
        Id = id;
    }
}