using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointRemoveRequests.Commands;

public record AcceptTravelPointRemoveRequest(Guid RequestId) : ICommand;