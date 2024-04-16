using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Payments.Application.TravelSummaries.Events.External;

public record TravelIsFinished(Guid travelId) : IEvent;
