using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Commands;

public record AcceptTravelPointUpdateRequest(Guid requestId) : ICommand;
