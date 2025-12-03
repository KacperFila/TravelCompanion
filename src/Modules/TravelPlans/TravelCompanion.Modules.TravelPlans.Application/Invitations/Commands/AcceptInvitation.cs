using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands;

public record AcceptInvitation(Guid InvitationId) : ICommand;
