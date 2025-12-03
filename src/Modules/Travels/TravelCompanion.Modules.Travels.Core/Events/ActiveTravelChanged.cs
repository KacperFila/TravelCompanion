using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Travels.Core.Events;

public record ActiveTravelChanged(Guid UserId, Guid TravelId) : IEvent;