using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands;

public record InviteToTravelPlan(Guid travelPlanId, Guid userId) : ICommand;