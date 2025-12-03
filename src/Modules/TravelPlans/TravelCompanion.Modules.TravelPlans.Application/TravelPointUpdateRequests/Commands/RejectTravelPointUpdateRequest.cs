using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointUpdateRequests.Commands;

public record RejectTravelPointUpdateRequest(Guid RequestId) : ICommand;