using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

public class PointAlreadyAcceptedException : TravelCompanionException
{
    public Guid pointId { get; set; }
    public PointAlreadyAcceptedException(Guid pointId) : base($"Point with Id: {pointId} is already accepted.")
    {
        pointId = pointId;
    }
}