using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Commands;

public record DenyPlanAcceptRequest(Guid TravelPlanId) : ICommand;