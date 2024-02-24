using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

public class TravelPoint : AggregateRoot
{
    public string PlaceName { get; init; }

    public TravelPoint(AggregateId id, string placeName)
    {
        Id = id;
        PlaceName = placeName;
    }

    public static TravelPoint Create(Guid id, string placeName)
    => new (id, placeName);
}