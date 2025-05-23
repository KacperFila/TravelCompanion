using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Payments.Application.TravelSummaries.Events.External;

public record TravelFinished(Guid TravelId) : IEvent;
