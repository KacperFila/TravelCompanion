using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlanInvitations.Commands;

public record AcceptInvitationToTravelPlan(Guid invitationId) : ICommand;
