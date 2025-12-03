using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands;

public record InviteToTravelPlan(Guid PlanId, Guid InviteeId) : ICommand;