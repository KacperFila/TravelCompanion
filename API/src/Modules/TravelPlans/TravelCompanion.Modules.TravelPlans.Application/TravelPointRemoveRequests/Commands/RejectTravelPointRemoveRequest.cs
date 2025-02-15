using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointRemoveRequests.Commands;

public record RejectTravelPointRemoveRequest(Guid requestId) : ICommand;