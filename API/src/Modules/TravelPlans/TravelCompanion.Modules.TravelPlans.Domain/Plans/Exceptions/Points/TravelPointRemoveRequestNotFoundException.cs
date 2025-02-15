using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

public class TravelPointRemoveRequestNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public TravelPointRemoveRequestNotFoundException(Guid id) : base($"Travel point remove request with Id: {id} was not found.")
    {
        Id = id;
    }
}