using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

internal class InvalidPointOrderException : TravelCompanionException
{
    public Guid PointId { get; set; }
    public InvalidPointOrderException(Guid pointId) : base($"Order number for point: {pointId} is not valid.")
    {
        PointId = pointId;
    }
}
