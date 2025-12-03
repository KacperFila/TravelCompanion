using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Travels.Core.Events;

public record TravelIsFinished(Guid TravelId) : IEvent;
