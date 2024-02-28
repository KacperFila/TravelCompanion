using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class EmptyTravelPointPlaceNameException : TravelCompanionException
{
    public Guid Id { get; set; }
    public EmptyTravelPointPlaceNameException(Guid id) : base($"Travel point with Id:{id} defines an empty place name.")
    {
        Id = id;
    }
}