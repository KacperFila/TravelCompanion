using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Invitations;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class RejectInvitationToTravelPlanHandler : ICommandHandler<RejectInvitation>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public RejectInvitationToTravelPlanHandler(IInvitationRepository invitationRepository, IContext context)
    {
        _invitationRepository = invitationRepository;
        _context = context;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(RejectInvitation command)
    {
        var invitation = await _invitationRepository.GetAsync(command.InvitationId);

        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.InvitationId);
        }

        if (_userId != invitation.InviteeId)
        {
            throw new UserNotAllowedToManageInvitationException(_userId);
        }

        await _invitationRepository.RemoveAsync(command.InvitationId);
    }
}